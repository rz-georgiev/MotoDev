using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Common.Extensions;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.Persistence;
using MotoDev.Infrastructure.Persistence.Migrations;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;

namespace MotoDev.Application.Services
{
    public class DashboardService(IHttpContextAccessor accessor,
        IUserService userService,
        MotoDevDbContext dbContext) : IDashboardService
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IUserService _userService = userService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<DashboardResponse>> GetDashboardDataAsync()
        {
            var repairShopsIds = _dbContext.RepairShops.Where(x => x.OwnerUserId == _userService.CurrentUserId).Select(x => x.Id).ToList();
            var usersIds = _dbContext.RepairShopUsers.Where(x => repairShopsIds.Contains(x.RepairShopId)).Select(x => x.UserId).ToList();

            var clientsUsersIds = _dbContext.Users.Where(x => usersIds.Contains(x.Id) && x.RoleId == (int)RoleOption.Client).Select(x => x.Id).ToList();
            var clients = _dbContext.Clients.Where(x => clientsUsersIds.Contains(x.UserId)).ToList();

            var clientsCars = await _dbContext.ClientCars.Where(x => clients.Select(x => x.Id).Contains(x.ClientId))
                .Select(x => new
                {
                    x.Id,
                    x.LicensePlateNumber,
                    x.IsActive
                })
                .Where(x => x.IsActive)
                .ToListAsync();

            var clientsCarsRepairs = await _dbContext.ClientCarRepairs.Where(x => clientsCars.Select(x => x.Id).Contains(x.ClientCarId))
            .Select(x => new
            {
                x.Id,
                x.CreatedAt,
                x.ClientCarId,
                x.RepairStatusId,
                x.IsActive
            })
            .Where(x => x.IsActive)
            .ToListAsync();

            var clientCarsRepairsDetails = await _dbContext.ClientCarRepairsDetails
                .Where(x => clientsCarsRepairs.Select(x => x.Id).Contains(x.ClientCarRepairId) && x.IsActive)
                .ToListAsync();

            var repairTypes = await _dbContext.RepairTypes.ToListAsync();
            var now = DateTime.UtcNow;

            var dashboardRecentActivity = new List<DashboardRecentActivity>();
            var dashboardReports = new DashboardReports
            {
                Dates = new List<DateTime>(),
                Repairs = new List<int>(),
                TotalProfits = new List<decimal>()
            };

            var lastSixRepairs = clientsCarsRepairs.Where(x => x.IsActive).OrderByDescending(x => x.CreatedAt).Take(6).ToList();
            foreach (var clientCarRepair in lastSixRepairs)
            {
                var clientCar = clientsCars.SingleOrDefault(x => x.Id == clientCarRepair.ClientCarId);
                var repairTypeId = clientCarsRepairsDetails?.LastOrDefault(x => x.ClientCarRepairId == clientCarRepair.Id)?.RepairTypeId;

                dashboardRecentActivity.Add(new DashboardRecentActivity
                {
                    Time = GetTime(clientCarRepair.CreatedAt),
                    Title = $"{clientCar.LicensePlateNumber} -> Status: {((RepairStatusOption)clientCarRepair.RepairStatusId).GetDisplayName()}",
                    RepairStatusId = clientCarRepair.RepairStatusId,
                });
            }

            for (var index = 0; index < 12; index++)
            {
                var previousMonth = now.AddMonths(-index);
                var monthStart = new DateTime(previousMonth.Year, previousMonth.Month, 1);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);
                var monthRepairs = clientCarsRepairsDetails.Where(x => 
                x.CreatedAt >= monthStart && 
                x.CreatedAt <= monthEnd && 
                x.IsActive);

                dashboardReports.Dates.Add(monthStart);
                dashboardReports.Repairs.Add(monthRepairs.Count());
                dashboardReports.TotalProfits.Add(monthRepairs.Sum(x => x.Price));
            }

            var response = new DashboardResponse
            {
                RepairsThisYear = clientCarsRepairsDetails.Count(x => x.CreatedAt.Year == now.Year),
                RepairsIncreaseThisYear = GetRepairsIncrease(clientCarsRepairsDetails),

                RevenueThisMonth = Convert.ToInt32(clientCarsRepairsDetails
                    .Where(x => x.CreatedAt.Year == now.Year
                         && x.CreatedAt.Month == now.Month 
                         && x.RepairStatusId == (int)RepairStatusOption.Done)
                    .Sum(x => x.Price)),

                RevenueIncreaseThisMonth = GetRevenueIncrease(clientCarsRepairsDetails),
                CustomersTotal = clients.Count(),
                CustomersIncreaseThisYear = GetCustomersIncrease(clients),
                DashboardRecentActivity = dashboardRecentActivity,
                DashboardReports = dashboardReports,
            };

            return new BaseResponse<DashboardResponse>
            {
                IsOk = true,
                Result = response,
            };
        }

        private string GetTime(DateTime startedDateTime)
        {
            var now = DateTime.UtcNow;
            var difference = now - startedDateTime;

            if (difference.TotalSeconds < 60)
                return $"{difference.Seconds} {(difference.Seconds == 1 ? "second" : "seconds")} ago";
            if (difference.TotalMinutes < 60)
                return $"{difference.Minutes} {(difference.Minutes == 1 ? "minute" : "minutes")} ago";
            if (difference.TotalHours < 24)
                return $"{difference.Hours} {(difference.Hours == 1 ? "hour" : "hours")} ago";
            if (difference.TotalDays < 7)
                return $"{difference.Days} {(difference.Days == 1 ? "day" : "days")} ago";
            if (difference.TotalDays < 30)
                return $"{difference.Days / 7} {(difference.Days / 7 == 1 ? "week" : "weeks")} ago";
            if (difference.TotalDays < 365)
                return $"{difference.Days / 30} {(difference.Days / 30 == 1 ? "month" : "months")} ago";

            return $"{difference.Days / 365} {(difference.Days / 365 == 1 ? "year" : "years")} ago";
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
                return 100;

            var result = ((decimal)(thisYear - previousYear) / previousYear);
            return (int)(result * 100);
        }
    }
}