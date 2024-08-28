using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class UserService(IConfiguration configuration,
        IEmailService emailService,
        MotoDevDbContext dbContext) : IUserService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId)
        {
            var repairShops = await _dbContext.RepairShops.Where(x => x.OwnerUserId == ownerUserId)
                .SelectMany(repairShop => repairShop.RepairShopUsers, (RepairShop, WorkingUser) => new { RepairShop, WorkingUser })
                .Select(x => new UserResponse
                {
                    Id = 0,
                    FirstName = "",
                    LastName = "",
                    RepairShop = "",
                    Position = "",
                }).ToListAsync();

            return new BaseResponse<IEnumerable<UserResponse>> { Result = repairShops };
        }
    }
}