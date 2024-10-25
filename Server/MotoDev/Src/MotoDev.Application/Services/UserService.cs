﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Common.Extensions;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class UserService(IConfiguration configuration,
        IEmailService emailService,
        IAccountService accountService,
        IMapper mapper,
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext,
        ICloudinaryService cloudinaryService) : IUserService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IAccountService _accountService = accountService;
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _accessor = accessor;

        public async Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId)
        {
            var result = new List<UserResponse>();
            var repairShops = await _dbContext.RepairShops.Where(x => x.OwnerUserId == ownerUserId && x.IsActive)
                .Include(x => x.RepairShopUsers.Where(s => s.IsActive && s.UserId != ownerUserId))
                    .ThenInclude(x => x.User)
                        .ThenInclude(x => x.Role)
                .ToListAsync();

            foreach (var repairShop in repairShops)
            {
                foreach (var repairShopUser in repairShop.RepairShopUsers)
                {
                    result.Add(new UserResponse
                    {
                        RepairShopUserId = repairShopUser.Id,
                        FirstName = repairShopUser.User?.FirstName ?? string.Empty,
                        LastName = repairShopUser.User?.LastName ?? string.Empty,
                        Position = string.Join(", ", repairShopUser?.User?.Role.Name),
                        RepairShop = repairShop.Name
                    });
                }
            }

            return new BaseResponse<IEnumerable<UserResponse>> { Result = result };
        }

        public async Task<BaseResponse> DeactivateRepairUserByIdAsync(int id)
        {
            try
            {
                var repairUser = _dbContext.RepairShopUsers.SingleOrDefault(x => x.Id == id);
                repairUser!.IsActive = false;

                var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == repairUser.UserId);
                user.IsActive = false;

                await _dbContext.SaveChangesAsync();

                return ResponseHelper.Success("Successfully deactivated record");

            }
            catch (Exception)
            {
                return ResponseHelper.Failure("An error occurred while trying to deactivate the record");
            }
        }

        public async Task<BaseResponse<UserResponse>> EditAsync(UserRequest request)
        {
            var doesRoleExist = _dbContext.Roles.Any(x => x.Id == request.RoleId);
            var doesRepairShopExist = _dbContext.RepairShops.Any(x => x.Id == request.RepairShopId);

            if (!doesRepairShopExist || !doesRepairShopExist)
            {
                return ResponseHelper.Failure(new UserResponse { }, "Provided role and/or repair shop do not exist");
            }

            if (request.RepairShopUserId == null)
            {
                return await AddAsync(request);
            }
            else
            {
                return await ChangeAsync(request);
            }
        }

        public async Task<BaseResponse<UserResponse>> EditMinimizedAsync(UserMinimizedRequest request)
        {
            var user = await _dbContext.Users.Where(x => x.Id == CurrentUserId)
                .Include(x => x.Role)
                .SingleOrDefaultAsync();

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.Password = request.Password.GenerateHash();
            }

            var refreshToken = _accountService.GenerateToken(user);
            user.RefreshToken = refreshToken;

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            var response = new UserResponse
            {
                RefreshToken = refreshToken
            };

            return ResponseHelper.Success(response);

        }

        private async Task<BaseResponse<UserResponse>> AddAsync(UserRequest request)
        {
            var userResponse = await _accountService.RegisterAsync(new RegisterAccountRequest
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            });

            if (userResponse.IsOk)
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

                //user = _mapper.Map<UserRequest, User>(request);
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;
                user.Username = request.Username;
                user.RoleId = request.RoleId;
                user.IsActive = true;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                var repairShopUser = new RepairShopUser
                {
                    UserId = user.Id,
                    RepairShopId = request.RepairShopId,
                    CreatedByUserId = CurrentUserId
                };

                if (request.RoleId == (int)RoleOption.Client)
                {
                    var client = new Client
                    {
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        CreatedByUserId = CurrentUserId,
                    };
                    await _dbContext.Clients.AddAsync(client);
                }

                await _dbContext.RepairShopUsers.AddAsync(repairShopUser);
                await _dbContext.SaveChangesAsync();

                var response = new UserResponse
                {
                    RepairShopUserId = repairShopUser.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Position = (await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == user.RoleId))!.Name,
                    RepairShop = (await _dbContext.RepairShops.SingleOrDefaultAsync(x => x.Id == request.RepairShopId))!.Name,
                };

                return ResponseHelper.Success(response);

            }
            else
            {
                return ResponseHelper.Failure(new UserResponse { }, userResponse.Message);
            }
        }

        private async Task<BaseResponse<UserResponse>> ChangeAsync(UserRequest request)
        {
            var repairShopUser = await _dbContext.RepairShopUsers.SingleOrDefaultAsync(x => x.Id == request.RepairShopUserId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == repairShopUser.UserId);

            if (request.RoleId == (int)RoleOption.Client)
            {
                var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.UserId == user.Id);
                if (client == null)
                {
                    client = new Client
                    {
                        UserId = user.Id,
                        CreatedAt = DateTime.UtcNow,
                        CreatedByUserId = CurrentUserId,
                    };
                    await _dbContext.Clients.AddAsync(client);
                }
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Username = request.Username;
            user.Password = string.IsNullOrWhiteSpace(request.Password) ? user.Password : request.Password.GenerateHash(); // if in edit mode by owner/administrator
            user.RoleId = request.RoleId;

            if (request.RepairShopId != repairShopUser.RepairShopId)
            {
                var isAlreadyAssignedToTheSpecifiedPlace = _dbContext.RepairShopUsers.Any(x => x.UserId == repairShopUser.UserId &&
                x.RepairShopId == request.RepairShopId);

                if (isAlreadyAssignedToTheSpecifiedPlace)
                {
                    return ResponseHelper.Failure(new UserResponse { },
                        "User is already assigned to the specified place");
                }

                _dbContext.Remove(repairShopUser);
                _dbContext.Add(new RepairShopUser
                {
                    UserId = user.Id,
                    RepairShopId = request.RepairShopId,
                    CreatedByUserId = CurrentUserId,
                });
            }

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Success<UserResponse>(new UserResponse { }, "Successfully changed");
        }

        public async Task<BaseResponse<UserExtendedResponse>> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            var response = new BaseResponse<UserExtendedResponse>
            {
                Result = _mapper.Map<UserExtendedResponse>(user)
            };

            if (user.ImageId == null)
                response.Result.ImageUrl = _configuration["DefaultUserImageUrl"];
            else
            {
                var url = _cloudinaryService.GetImageUrlById(user.ImageId);
                response.Result.ImageUrl = url;
            }

            return response;
        }

        public async Task<BaseResponse<UserProfileImageUpdateResponse>> UpdateProfileImage(IFormFile file)
        {
            var user = await _dbContext.Users.Where(x => x.Id == CurrentUserId)
                .Include(x => x.Role)
                .SingleOrDefaultAsync();

            if (user!.ImageId != null)
                await _cloudinaryService.DeleteImageAsync(user.ImageId);

            var newImageId = await _cloudinaryService.UploadImageAsync(file);
            user.ImageId = newImageId;

            var imageUrl = _cloudinaryService.GetImageUrlById(newImageId);
            var refreshToken = _accountService.GenerateToken(user);

            user.RefreshToken = refreshToken;

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            var response = new UserProfileImageUpdateResponse
            {
                ImageUrl = imageUrl,
                RefreshToken = refreshToken,
            };

            return ResponseHelper.Success(response);

        }

        public async Task<BaseResponse<IEnumerable<MechanicUserResponse>>> GetMechanicUsersAsync()
        {
            var repairShopsIds = _dbContext.RepairShops.Where(x => x.OwnerUserId == CurrentUserId && x.IsActive)
                .Select(x => x.Id);

            var repairShopUsersIds = _dbContext.RepairShopUsers.Where(x => repairShopsIds.Contains(x.RepairShopId) && x.IsActive)
                .Select(x => x.UserId);

            var result = await _dbContext.Users.Where(x => repairShopUsersIds.Contains(x.Id) && x.IsActive
                   && x.RoleId == (int)RoleOption.Mechanic)
           .Select(x => new MechanicUserResponse
           {
               MechanicUserId = x.Id,
               Name = $"{x.FirstName} {x.LastName}"
           }).ToListAsync();

            return ResponseHelper.Success<IEnumerable<MechanicUserResponse>>(result);
        }

        public int CurrentUserId
        {
            get
            {
                return Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);
            }
        }
    }
}