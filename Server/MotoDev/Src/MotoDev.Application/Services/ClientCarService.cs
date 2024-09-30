using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.Persistence;
using System.Runtime.CompilerServices;

namespace MotoDev.Application.Services
{
    public class ClientCarService(
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IClientCarService
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<ClientCarListingReponse>>> GetAllClientCarsAsync()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);

            var repairShops = _dbContext.RepairShops.Where(x => x.OwnerUserId == userId)
                .Include(x => x.RepairShopUsers.Where(x => x.User.RoleId == (int)RoleOption.Client))
                .ThenInclude(x => x.User);

            var usersIds = repairShops.SelectMany(x => x.RepairShopUsers.Select(x => x.User.Id));
            var clients = await _dbContext.Clients.Where(x => usersIds.Contains(x.UserId))
                .Include(x => x.ClientCars.Where(x => x.IsActive))
                .ThenInclude(x => x.Car)
                .ThenInclude(x => x.Model)
                .ThenInclude(x => x.Brand)
                .Include(x => x.User).ToListAsync();

            var clientsCars = clients.SelectMany(x => x.ClientCars);
            var result = clientsCars.Select(x => new ClientCarListingReponse
            {
                ClientCarId = x.Id,
                CarName = $"{x.Car.Brand.Name} {x.Car.Model.Name} {x.Car.Year}",
                ClientName = $"{x.Client.User.FirstName} {x.Client.User.LastName}",
                LicensePlateNumber = x.LicensePlateNumber,
            });

            return new BaseResponse<IEnumerable<ClientCarListingReponse>>
            {
                IsOk = true,
                Result = result,
            };
        }

        public async Task<BaseResponse<ClientCarEditDto>> GetByIdAsync(int clientCarId)
        {
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == clientCarId);
            return new BaseResponse<ClientCarEditDto>
            {
                IsOk = true,
                Result = new ClientCarEditDto
                {
                    ClientCarId = clientCar.Id,
                    CarId = clientCar.CarId,
                    ClientId = clientCar.ClientId,
                    LicensePlateNumber = clientCar.LicensePlateNumber
                }
            };
        }

        public async Task<BaseResponse<ClientCarListingReponse>> EditAsync(ClientCarEditDto request)
        {
            int userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);
           
            var currentClientCar = request.ClientCarId > 0 
                ? await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == request.ClientCarId)
                : new ClientCar();

            currentClientCar.ClientId = request.ClientId;
            currentClientCar.CarId = request.CarId;
            currentClientCar.LicensePlateNumber = request.LicensePlateNumber;

            if (request.ClientCarId > 0)
            {
                currentClientCar.LastUpdatedAt = DateTime.UtcNow;
                currentClientCar.LastUpdatedByUserId = userId;
                
                _dbContext.Update(currentClientCar);
            }
            else
            {
                currentClientCar.CreatedAt = DateTime.UtcNow;
                currentClientCar.CreatedByUserId = userId;
                currentClientCar.IsActive = true;

                await _dbContext.AddAsync(currentClientCar);
            }
            
            await _dbContext.SaveChangesAsync();


            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == currentClientCar.ClientId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == client.UserId);
            var car = await _dbContext.Cars.Where(x => x.Id == currentClientCar.CarId)
                .Include(x => x.Brand)
                .Include(x => x.Model).SingleOrDefaultAsync();
             

            return new BaseResponse<ClientCarListingReponse>
            {
                IsOk = true,
                Message = "",
                Result = new ClientCarListingReponse
                {
                    ClientCarId = currentClientCar.Id,
                    CarName = $"{car.Brand.Name} {car.Model.Name} {car.Year}",
                    ClientName = $"{user.FirstName} {user.LastName}",
                    LicensePlateNumber = currentClientCar.LicensePlateNumber,
                }
            };
        }

        public async Task<BaseResponse<bool>> DeactivateByCarRepairIdAsync(int clientCarId)
        {
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == clientCarId);
            clientCar.IsActive = false;
          
            _dbContext.Update(clientCar);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>
            {
                IsOk = true,
                Result = true
            };
        }

        public async Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCarsAsync(int clientId)
        {
            var data = await _dbContext.ClientCars.Where(x => x.ClientId == clientId)
                .Include(x => x.Car)
                .ThenInclude(x => x.Model)
                .ThenInclude(x => x.Brand)
                .ToListAsync();

            var result = data.Select(x => new ClientCarResponse
            {
                ClientCarId = x.Id,
                CarName = $"{x.Car.Brand.Name} {x.Car.Model.Name} {x.Car.Year} -> {x.LicensePlateNumber}"
            });

            return new BaseResponse<IEnumerable<ClientCarResponse>>
            {
                IsOk = true,
                Result = result
            };
        }
    }
}