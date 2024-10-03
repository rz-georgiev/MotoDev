using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;
using System.Collections.Generic;

namespace MotoDev.Application.Services
{
    public class CarRepairService(IConfiguration configuration,
        IEmailService emailService,
        IHttpContextAccessor accessor,
        ICloudinaryService cloudinaryService,
        IUserService userService,
        MotoDevDbContext dbContext) : ICarRepairService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly IUserService _userService = userService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarsRepairsAsync()
        {
            var result = new List<CarRepairResponse>();

            var repairShops = await _dbContext.RepairShops.Where(x => x.OwnerUserId == _userService.CurrentUserId)
                .Include(x => x.RepairShopUsers.Where(x => x.User.RoleId == (int)RoleOption.Client && x.User.IsActive))
                .ThenInclude(x => x.User).ToListAsync();

            var usersIds = repairShops.SelectMany(x => x.RepairShopUsers.Select(x => x.UserId));
            var clients = await _dbContext.Clients.Where(x => usersIds.Contains(x.UserId) && x.User.IsActive)
                .Include(x => x.ClientCars)
                .ThenInclude(x => x.ClientCarRepairs.Where(x => x.IsActive == true))
                .ThenInclude(x => x.RepairStatus)
                .Include(x => x.ClientCars)
                .ThenInclude(x => x.Car).ToListAsync();

            foreach (var repairShop in repairShops)
            {
                foreach (var repairShopUser in repairShop.RepairShopUsers)
                {
                    var currentClient = clients.SingleOrDefault(x => x.UserId == repairShopUser.UserId);
                    foreach (var clientCar in currentClient.ClientCars)
                    {
                        foreach (var clientCarRepair in clientCar.ClientCarRepairs)
                        {
                            result.Add(new CarRepairResponse
                            {
                                FirstName = repairShopUser.User.FirstName,
                                LastName = repairShopUser.User.LastName,
                                LicensePlateNumber = clientCar.LicensePlateNumber,
                                CarRepairId = clientCarRepair.Id,
                                Status = clientCarRepair.RepairStatus.Name,
                                StatusId = clientCarRepair.RepairStatusId,
                                RepairDateTime = clientCarRepair.CreatedAt,
                            });
                        }
                    }
                }
            }

            return ResponseHelper.Success<IEnumerable<CarRepairResponse>>(result);
        }

        public async Task<BaseResponse<CarRepairResponse>> EditAsync(CarRepairRequest request)
        {
            var currentClientCarRepair = request.CarRepairId > 0
                ? await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == request.CarRepairId)
                : new ClientCarRepair();

            currentClientCarRepair.ClientCarId = request.ClientCarId;
            currentClientCarRepair.PerformedByMechanicUserId = request.MechanicUserId;

            if (request.CarRepairId > 0)
            {
                currentClientCarRepair.LastUpdatedAt = DateTime.UtcNow;
                currentClientCarRepair.LastUpdatedByUserId = _userService.CurrentUserId;

                _dbContext.Update(currentClientCarRepair);
            }
            else
            {
                currentClientCarRepair.RepairStatusId = (int)RepairStatusOption.ToDo;
                currentClientCarRepair.CreatedAt = DateTime.UtcNow;
                currentClientCarRepair.CreatedByUserId = _userService.CurrentUserId;
                currentClientCarRepair.IsActive = true;

                await _dbContext.AddAsync(currentClientCarRepair);
            }

            await _dbContext.SaveChangesAsync();

            var clientCar = await
                _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == currentClientCarRepair.ClientCarId);

            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == clientCar.ClientId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == client.UserId);
            var repairStatus = _dbContext.RepairStatuses.SingleOrDefault(x => x.Id == currentClientCarRepair.RepairStatusId);

            var response = new CarRepairResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                LicensePlateNumber = clientCar.LicensePlateNumber,
                CarRepairId = currentClientCarRepair.Id,
                Status = repairStatus.Name,
                StatusId = repairStatus.Id,
                RepairDateTime = currentClientCarRepair.CreatedAt,
            };
            return ResponseHelper.Success(response);
        }

        public async Task<BaseResponse<CarRepairEditResponse>> GetByIdAsync(int carRepairId)
        {
            var carRepair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == carRepairId);
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == carRepair.ClientCarId);

            var response = new CarRepairEditResponse
            {
                CarRepairId = carRepair.Id,
                ClientCarId = clientCar.Id,
                ClientId = clientCar.ClientId,
                MechanicUserId = carRepair.PerformedByMechanicUserId,
            };

            return ResponseHelper.Success(response);
        }

        public async Task<BaseResponse<bool>> DeactivateByCarRepairIdAsync(int carRepairId)
        {
            var carRepair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == carRepairId);
            carRepair.IsActive = false;

            var details = await _dbContext.ClientCarRepairsDetails.Where(x => x.ClientCarRepairId == carRepair.Id).ToListAsync();
            details.ForEach(x => x.IsActive = false);

            _dbContext.Update(carRepair);
            _dbContext.UpdateRange(details);

            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Success(true);
        }

        public async Task<BaseResponse<IEnumerable<CarRepairSelectResponse>>> GetClientsRepairsAsync()
        {
            var data = await GetAllCarsRepairsAsync();
            var result = data.Result.Select(x => new CarRepairSelectResponse
            {
                ClientCarRepairId = x.CarRepairId,
                RepairName = $"#{x.CarRepairId} / {x.FirstName} {x.LastName} / {x.LicensePlateNumber}"
            })
            .OrderBy(x => x.ClientCarRepairId);

            return ResponseHelper.Success<IEnumerable<CarRepairSelectResponse>>(result);

        }
    }
}