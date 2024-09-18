using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class DashboardService(IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IDashboardService
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<DashboardResponse>> GetDashboardData()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);
            
            var repairShopsIds = _dbContext.RepairShops.Where(x => x.OwnerUserId == userId).Select(x => x.Id);
            var usersIds = _dbContext.RepairShopUsers.Where(x => repairShopsIds.Contains(x.UserId)).Select(x => x.UserId);
            var clientsUsersIds = _dbContext.Users.Where(x => usersIds.Contains(x.Id) && x.RoleId == (int)RoleOption.Client).Select(x => x.Id);
            var clientsIds = _dbContext.Clients.Where(x => clientsUsersIds.Contains(x.UserId)).Select(x => x.Id);
            var clientsCarsIds = _dbContext.ClientCars.Where(x => clientsIds.Contains(x.ClientId)).Select(x => x.Id);
            var clientsCarsRepairs = _dbContext.ClientCarRepairs.Where(x => clientsCarsIds.Contains(x.ClientCarId))
            .Select(x => new
            {
                x.Id,
                x.CreatedAt
            });

            var clientCarsRepairsDetails = _dbContext.ClientCarRepairsDetails
                .Where(x => clientsCarsRepairs
                .Select(x => x.Id)
                .Contains(x.ClientCarRepairId))
                .ToList();
            
            var now = DateTime.UtcNow;
            
            var dashboardRecentActivity = new List<DashboardRecentActivity>();
            var a = clientCarsRepairsDetails.OrderByDescending(x => x.CreatedAt);

            var response = new DashboardResponse
            {
                RepairsThisYear = clientsCarsRepairs.Count(x => x.CreatedAt.Year == now.Year),
                RepairsIncreaseThisYear = 0,

                RevenueThisMonth = Convert.ToInt32(clientCarsRepairsDetails
                    .Where(x => x.CreatedAt.Year == now.Year && x.CreatedAt.Month == now.Month)
                    .Sum(x => x.Price)),

                RevenueIncreaseThisMonth = 0,
                CustomersTotal = clientsIds.Count(),
                CustomersIncreaseThisYear = 0,
                DashboardRecentActivity = new List<DashboardRecentActivity>
                {
                    
                }
            };

            return null;
        }
    }
}