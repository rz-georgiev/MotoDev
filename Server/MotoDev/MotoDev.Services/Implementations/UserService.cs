using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MotoDev.Core.Dtos;
using MotoDev.Data;
using MotoDev.Services.Interaces;

namespace MotoDev.Services.Implementations
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
                .SelectMany(repairShop => repairShop.WorkingUsers, (RepairShop, WorkingUser) => new { RepairShop, WorkingUser })
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