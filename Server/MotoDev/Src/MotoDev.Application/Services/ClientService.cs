using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class ClientService(
        IHttpContextAccessor accessor,
        IUserService userService,
        MotoDevDbContext dbContext) : IClientService
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IUserService _userService = userService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<ClientResponse>>> GetAllClientsAsync()
        {
            var repairShops = _dbContext.RepairShops.Where(x => x.OwnerUserId == _userService.CurrentUserId && x.IsActive);

            var repairShopUsers = _dbContext.RepairShopUsers.Where(x => repairShops.Select(x => x.Id).Contains(x.RepairShopId) && x.IsActive).ToList();
            var users = await _dbContext.Users.Where(x => repairShopUsers.Select(x => x.UserId).Contains(x.Id) && x.RoleId == (int)RoleOption.Client
                && x.IsActive)
                .ToListAsync();

            var clients = await _dbContext.Clients.Where(x => users.Select(x => x.Id).Contains(x.UserId))
                .ToListAsync(); 

            var result = clients.Select(client => new ClientResponse
            {
                ClientId = client.Id,
                FullName = $"{users.SingleOrDefault(x => x.Id == client.UserId)!.FirstName}" +
                $" {users.SingleOrDefault(x => x.Id == client.UserId)!.LastName}"
            });

            return new BaseResponse<IEnumerable<ClientResponse>>
            {
                IsOk = true,
                Result = result
            };
        }

        public async Task<BaseResponse<IEnumerable<ClientCarStatusResponse>>> GetMyCarsStatusesAsync()
        {
            var clientData = await _dbContext.Clients.Where(x => x.UserId == _userService.CurrentUserId)
                .Include(x => x.ClientCars.Where(x => x.IsActive))
                    .ThenInclude(x => x.Car)
                    .ThenInclude(x => x.CarType)
                    .Include(x => x.ClientCars)
                    .ThenInclude(x => x.Car)
                    .ThenInclude(x => x.Model)
                    .ThenInclude(x => x.Brand)
                .Include(x => x.ClientCars)
                    .ThenInclude(x => x.ClientCarRepairs)
                    .ThenInclude(x => x.ClientCarRepairsDetails)
                    .ThenInclude(x => x.RepairStatus)
                .Include(x => x.ClientCars)
                    .ThenInclude(x => x.ClientCarRepairs)
                    .ThenInclude(x => x.ClientCarRepairsDetails)
                    .ThenInclude(x => x.RepairType)
                .ToListAsync();

            var result = new List<ClientCarStatusResponse>();
            foreach (var client in clientData)
            {
                foreach (var clientCar in client.ClientCars)
                {
                    result.Add(new ClientCarStatusResponse
                    {
                        LicensePlateNumber = clientCar.LicensePlateNumber,
                        VehicleName = $"{clientCar.Car.Brand.Name} {clientCar.Car.Model.Name} " +
                        $"/ {clientCar.Car.Year}" +
                        $" / {clientCar.Car.CarType.Name}",
                        Details = []
                    });

                    foreach (var repair in clientCar.ClientCarRepairs)
                    {
                        foreach (var detail in repair.ClientCarRepairsDetails)
                        {
                            result!.LastOrDefault()!.Details.Add(new ClientCarStatusDetailResponse
                            {
                                RepairName = detail.RepairType.Name,
                                RepairStartDateTime = detail.RepairStartDateTime,
                                RepairEndDateTime = detail.RepairEndDateTime,
                                StatusId = detail.RepairStatusId,
                                StatusName = detail.RepairStatus.Name
                            });
                        }
                    }
                }
            }

            return new BaseResponse<IEnumerable<ClientCarStatusResponse>>
            {
                IsOk = true,
                Result = result,
            };
        }
    }
}