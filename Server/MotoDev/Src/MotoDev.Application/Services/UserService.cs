using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
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
        MotoDevDbContext dbContext) : IUserService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IAccountService _accountService = accountService;
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _accessor = accessor;

        public async Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId)
        {
            var result = new List<UserResponse>();
            var repairShops = await _dbContext.RepairShops.Where(x => x.OwnerUserId == ownerUserId)
                .Include(x => x.RepairShopUsers.Where(s => s.IsActive))
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
                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    IsOk = true,
                    Message = "Successfully deactivated record"
                };
            }
            catch (Exception)
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "An error occurred while trying to deactivate the record"
                };
            }
        }

        public async Task<BaseResponse<UserResponse>> EditAsync(UserRequest request)
        {
            var doesRoleExist = _dbContext.Roles.Any(x => x.Id == request.RoleId);
            var doesRepairShopExist = _dbContext.RepairShops.Any(x => x.Id == request.RepairShopId);

            if (!doesRepairShopExist || !doesRepairShopExist)
            {
                return new BaseResponse<UserResponse>
                {
                    IsOk = false,
                    Message = "Provided role and/or repair shop do not exist",
                };
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
                    CreatedByUserId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value)
                };

                await _dbContext.RepairShopUsers.AddAsync(repairShopUser);
                await _dbContext.SaveChangesAsync();

                return new BaseResponse<UserResponse>
                {
                    IsOk = true,
                    Message = "",
                    Result = new UserResponse
                    {
                        RepairShopUserId = repairShopUser.Id,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Position = (await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == user.RoleId))!.Name,
                        RepairShop = (await _dbContext.RepairShops.SingleOrDefaultAsync(x => x.Id == request.RepairShopId))!.Name,
                    }
                };
            }
            else
            {
                return new BaseResponse<UserResponse>
                {
                    IsOk = false,
                    Message = userResponse.Message,
                };
            }
        }

        private async Task<BaseResponse<UserResponse>> ChangeAsync(UserRequest request)
        {
            var repairShopUser = await _dbContext.RepairShopUsers.SingleOrDefaultAsync(x => x.Id == request.RepairShopUserId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == repairShopUser.UserId);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Username = request.Username;
            user.Password = request.Password.GenerateHash();
            user.RoleId = request.RoleId;

            if (request.RepairShopId != repairShopUser.RepairShopId)
            {
                var isAlreadyAssignedToTheSpecifiedPlace = _dbContext.RepairShopUsers.Any(x => x.UserId == repairShopUser.UserId &&
                x.RepairShopId == request.RepairShopId);

                if (isAlreadyAssignedToTheSpecifiedPlace)
                {
                    return new BaseResponse<UserResponse>
                    {
                        IsOk = false,
                        Message = "User is already assigned to the specified place"
                    };
                }

                _dbContext.Remove(repairShopUser);
                _dbContext.Add(new RepairShopUser
                {
                    UserId = user.Id,
                    RepairShopId = request.RepairShopId,
                    CreatedByUserId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value)
                });
            }

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<UserResponse>
            {
                IsOk = true,
                Message = "Successfully changed",
            };
        }

        public async Task<BaseResponse<UserExtendedResponse>> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            return new BaseResponse<UserExtendedResponse>
            {
                Result = _mapper.Map<UserExtendedResponse>(user)
            };
        }
    }
}