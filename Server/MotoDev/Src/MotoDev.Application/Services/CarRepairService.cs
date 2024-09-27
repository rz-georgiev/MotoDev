using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Constants;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Common.Extensions;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;
using MotoDev.Infrastructure.Persistence.Migrations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Application.Services
{
    public class CarRepairService(IConfiguration configuration,
        IEmailService emailService,
        IHttpContextAccessor accessor,
        ICloudinaryService cloudinaryService,
        MotoDevDbContext dbContext) : ICarRepairService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarsRepairsAsync()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);

            var result = new List<CarRepairResponse>();

            var repairShops = await _dbContext.RepairShops.Where(x => x.OwnerUserId == userId)
                .Include(x => x.RepairShopUsers.Where(x => x.User.RoleId == (int)RoleOption.Client))
                .ThenInclude(x => x.User).ToListAsync();

            var usersIds = repairShops.SelectMany(x => x.RepairShopUsers.Select(x => x.UserId));
            var clients = await _dbContext.Clients.Where(x => usersIds.Contains(x.UserId))
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

            return new BaseResponse<IEnumerable<CarRepairResponse>>
            {
                IsOk = true,
                Result = result
            };
        }
        
        public async Task<BaseResponse<CarRepairResponse>> EditAsync(CarRepairRequest request)
        {

            var currentClientCarRepair = new ClientCarRepair();
            int userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);

            if (request.CarRepairId > 0)
            {
                currentClientCarRepair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == request.CarRepairId);
                currentClientCarRepair.ClientCarId = request.ClientCarId;
                currentClientCarRepair.LastUpdatedAt = DateTime.UtcNow;
                currentClientCarRepair.LastUpdatedByUserId = userId;

                _dbContext.Update(currentClientCarRepair);
            }
            else
            {
                currentClientCarRepair.ClientCarId = request.ClientCarId;
                currentClientCarRepair.RepairStatusId = (int)RepairStatusOption.ToDo;
                currentClientCarRepair.CreatedAt = DateTime.UtcNow;
                currentClientCarRepair.CreatedByUserId = userId;

                await _dbContext.AddAsync(currentClientCarRepair);
            }

            await _dbContext.SaveChangesAsync();

            var clientCar = await
                _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == currentClientCarRepair.ClientCarId);

            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == clientCar.ClientId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == client.UserId);
            var repairStatus = _dbContext.RepairStatuses.SingleOrDefault(x => x.Id == currentClientCarRepair.RepairStatusId);

            return new BaseResponse<CarRepairResponse>
            {
                IsOk = true,
                Message = "",
                Result = new CarRepairResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    LicensePlateNumber = clientCar.LicensePlateNumber,
                    CarRepairId = currentClientCarRepair.Id,
                    Status = repairStatus.Name,
                    StatusId = repairStatus.Id,
                    RepairDateTime = currentClientCarRepair.CreatedAt,
                }
            };
        }

        public async Task<BaseResponse<CarRepairEditResponse>> GetByIdAsync(int carRepairId)
        {
            var carRepair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == carRepairId);
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == carRepair.ClientCarId);

            return new BaseResponse<CarRepairEditResponse>
            {
                IsOk = true,
                Result = new CarRepairEditResponse
                {
                    CarRepairId = carRepair.Id,
                    ClientCarId = clientCar.Id,
                    ClientId = clientCar.ClientId
                }
            };
        }

        public async Task<BaseResponse<bool>> DeactivateByCarRepairIdAsync(int carRepairId)
        {
            var carRepair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == carRepairId);
            carRepair.IsActive = false;
         
            _dbContext.Update(carRepair);
            await _dbContext.SaveChangesAsync();
            
            return new BaseResponse<bool>
            {
                IsOk = true,
                Result = true,
            };
        }
    }
}