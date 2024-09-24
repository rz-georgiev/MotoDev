using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class ClientService(
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IClientService
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<ClientCarStatusResponse>>> GetMyCarsStatusesAsync()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);

            //        var clientData = await _dbContext.Clients
            //.Where(x => x.UserId == userId)
            //.Select(client => new
            //{
            //    Client = client,
            //    Cars = client.ClientCars.Select(car => new
            //    {
            //        Car = car.Car,
            //        Model = car.Car.Model,
            //        Brand = car.Car.Model.Brand,
            //        Repairs = car.ClientCarRepairs.Select(repair => new
            //        {
            //            RepairDetails = repair.ClientCarRepairsDetails.Select(details => new
            //            {
            //                RepairStatus = details.RepairStatus,
            //                RepairTypes = details.RepairType
            //            })
            //        })
            //    })
            //})
            //.ToListAsync();

            var clientData = await _dbContext.Clients.Where(x => x.UserId == userId)
                .Include(x => x.ClientCars)
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