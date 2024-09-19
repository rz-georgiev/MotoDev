using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.Persistence;
using MotoDev.Infrastructure.Persistence.Migrations;
using System.Data;
using System.Linq;

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
            var usersIds = _dbContext.RepairShopUsers.Where(x => repairShopsIds.Contains(x.RepairShopId)).Select(x => x.UserId);
            var clientsUsersIds = _dbContext.Users.Where(x => usersIds.Contains(x.Id) && x.RoleId == (int)RoleOption.Client).Select(x => x.Id);
            var clients = _dbContext.Clients.Where(x => clientsUsersIds.Contains(x.UserId)).ToList();
            var clientsCarsIds = _dbContext.ClientCars.Where(x => clients.Select(x => x.Id).Contains(x.ClientId)).Select(x => x.Id);
            var clientsCarsRepairs = _dbContext.ClientCarRepairs.Where(x => clientsCarsIds.Contains(x.ClientCarId))
            .Select(x => new
            {
                x.Id,
                x.CreatedAt
            });

            var clientCarsRepairsDetails = await _dbContext.ClientCarRepairsDetails
                .Where(x => clientsCarsRepairs.Select(x => x.Id).Contains(x.ClientCarRepairId))
                .ToListAsync();
            
            var now = DateTime.UtcNow;
            
            var dashboardRecentActivity = new List<DashboardRecentActivity>();
            
            var response = new DashboardResponse
            {
                RepairsThisYear = clientCarsRepairsDetails.Count(x => x.CreatedAt.Year == now.Year),
                RepairsIncreaseThisYear = GetRepairsIncrease(clientCarsRepairsDetails),
                
                RevenueThisMonth = Convert.ToInt32(clientCarsRepairsDetails
                    .Where(x => x.CreatedAt.Year == now.Year && x.CreatedAt.Month == now.Month)
                    .Sum(x => x.Price)),
               
                RevenueIncreaseThisMonth = GetRevenueIncrease(clientCarsRepairsDetails),
                CustomersTotal = clients.Count(),
                CustomersIncreaseThisYear = GetCustomersIncrease(clients),
                DashboardRecentActivity = new List<DashboardRecentActivity>
                {
                    
                }
            };

            return new BaseResponse<DashboardResponse>
            {
                IsOk = true,
                Result = response,
            };
        }

        private int GetRevenueIncrease(IEnumerable<ClientCarRepairDetail> details)
        {
            var now = DateTime.UtcNow;
            var thisYear = details.Where(x => x.CreatedAt.Year == now.Year && x.CreatedAt.Month == now.Month).Sum(x => x.Price);
            var previousYear = details.Where(x => x.CreatedAt.Year == now.Year && x.CreatedAt.Month == now.Month - 1).Sum(x => x.Price);

            return GetResult(thisYear, previousYear);
        }

        private int GetRepairsIncrease(IEnumerable<ClientCarRepairDetail> details)
        {
            var now = DateTime.UtcNow;
            var thisYear = details.Count(x => x.CreatedAt.Year == now.Year);
            var previousYear = details.Count(x => x.CreatedAt.Year == now.Year - 1);

            return GetResult(thisYear, previousYear);
        }

        private int GetCustomersIncrease(IEnumerable<Client> clients)
        {
            var now = DateTime.UtcNow;
            var thisYear = clients.Count(x => x.CreatedAt.Year == now.Year);
            var previousYear = clients.Count(x => x.CreatedAt.Year == now.Year - 1);

            return GetResult(thisYear, previousYear);
        }

        private int GetResult(decimal thisYear, decimal previousYear)
        {
            if (previousYear == 0)
                previousYear += 1;

            var result = ((decimal)(thisYear - previousYear) / previousYear);
            return (int)(result * 100);
        }
      
    }
}