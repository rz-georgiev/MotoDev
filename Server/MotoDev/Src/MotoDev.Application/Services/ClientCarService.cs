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
        IUserService userService,
        MotoDevDbContext dbContext) : IClientCarService
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IUserService _userService = userService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<ClientCarListingReponse>>> GetAllClientCarsAsync()
        {
            var repairShops = _dbContext.RepairShops.Where(x => x.OwnerUserId == _userService.CurrentUserId && x.IsActive)
                .Include(x => x.RepairShopUsers.Where(x => x.IsActive && x.User.RoleId == (int)RoleOption.Client) )
                .ThenInclude(x => x.User);

            var usersIds = repairShops.SelectMany(x => x.RepairShopUsers.Select(x => x.User.Id));
            var clients = await _dbContext.Clients.Where(x => usersIds.Contains(x.UserId) && x.User.IsActive)
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

            return ResponseHelper.Success<IEnumerable<ClientCarListingReponse>>(result);

        }

        public async Task<BaseResponse<ClientCarEditDto>> GetByIdAsync(int clientCarId)
        {
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == clientCarId);
            var response = new ClientCarEditDto
            {
                ClientCarId = clientCar.Id,
                CarId = clientCar.CarId,
                ClientId = clientCar.ClientId,
                LicensePlateNumber = clientCar.LicensePlateNumber
            };

            return ResponseHelper.Success(response);

        }

        public async Task<BaseResponse<ClientCarListingReponse>> EditAsync(ClientCarEditDto request)
        {           
            var currentClientCar = request.ClientCarId > 0 
                ? await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == request.ClientCarId)
                : new ClientCar();

            currentClientCar.ClientId = request.ClientId;
            currentClientCar.CarId = request.CarId;
            currentClientCar.LicensePlateNumber = request.LicensePlateNumber;

            if (request.ClientCarId > 0)
            {
                currentClientCar.LastUpdatedAt = DateTime.UtcNow;
                currentClientCar.LastUpdatedByUserId = _userService.CurrentUserId;
                
                _dbContext.Update(currentClientCar);
            }
            else
            {
                currentClientCar.CreatedAt = DateTime.UtcNow;
                currentClientCar.CreatedByUserId = _userService.CurrentUserId;
                currentClientCar.IsActive = true;

                await _dbContext.AddAsync(currentClientCar);
            }
            
            await _dbContext.SaveChangesAsync();


            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == currentClientCar.ClientId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == client.UserId);
            var car = await _dbContext.Cars.Where(x => x.Id == currentClientCar.CarId)
                .Include(x => x.Brand)
                .Include(x => x.Model).SingleOrDefaultAsync();

            var response = new ClientCarListingReponse
            {
                ClientCarId = currentClientCar.Id,
                CarName = $"{car.Brand.Name} {car.Model.Name} {car.Year}",
                ClientName = $"{user.FirstName} {user.LastName}",
                LicensePlateNumber = currentClientCar.LicensePlateNumber,
            };

            return ResponseHelper.Success(response);

        }

        public async Task<BaseResponse<bool>> DeactivateByCarRepairIdAsync(int clientCarId)
        {
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == clientCarId);
            clientCar.IsActive = false;
          
            _dbContext.Update(clientCar);
            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Success(true);

        }

        public async Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCarsAsync(int clientId)
        {
            var data = await _dbContext.ClientCars.Where(x => x.ClientId == clientId && x.IsActive)
                .Include(x => x.Car)
                .ThenInclude(x => x.Model)
                .ThenInclude(x => x.Brand)
                .ToListAsync();

            var result = data.Select(x => new ClientCarResponse
            {
                ClientCarId = x.Id,
                CarName = $"{x.Car.Brand.Name} {x.Car.Model.Name} {x.Car.Year} -> {x.LicensePlateNumber}"
            });

            return ResponseHelper.Success(result);

        }
    }
}