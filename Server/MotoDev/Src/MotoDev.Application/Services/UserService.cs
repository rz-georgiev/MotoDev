using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class UserService(IConfiguration configuration,
        IEmailService emailService,
        IAccountService accountService,
        IMapper mapper,
        MotoDevDbContext dbContext) : IUserService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IAccountService _accountService = accountService;
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

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
                        Id = repairShopUser.Id,
                        FirstName = repairShopUser.User.FirstName,
                        LastName = repairShopUser.User.LastName,
                        Position = string.Join(", ", repairShopUser.User.Role.Name),
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

        public async Task<BaseResponse<UserResponse>> CreateAsync(UserRequest request)
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

            var userResponse = await _accountService.RegisterAsync(new RegisterAccountRequest
            {
                Email = request.Email,
                Password = request.Password,
            });
            if (userResponse.IsOk)
            {
                var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email);
                user!.IsActive = true;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;
                user.Username = request.Username;
                user.RoleId = request.RoleId;
               
                 _dbContext.Users.Update(user);
                await  _dbContext.SaveChangesAsync();
                
                var repairShopUser = new RepairShopUser
                {
                    UserId = user.Id,
                    RepairShopId = request.RepairShopId,
                };

                await _dbContext.RepairShopUsers.AddAsync(repairShopUser);
                await _dbContext.SaveChangesAsync();

                return new BaseResponse<UserResponse>
                {
                    IsOk = true,
                    Message = "",
                    Result = new UserResponse
                    {
                        Id = repairShopUser.Id,
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
    }
}