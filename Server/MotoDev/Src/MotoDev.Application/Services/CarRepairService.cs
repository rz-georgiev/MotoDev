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
        MotoDevDbContext dbContext)  : ICarRepairService
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
                .ThenInclude(x => x.ClientCarRepairs)
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

        public async Task<BaseResponse<IEnumerable<CarRepairEditResponse>>> GetByIdAsync(int carRepairId)
        {
            var carRepair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == carRepairId);
            var clientCar = await _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == carRepair.ClientCarId);
           

            var response = new CarRepairEditResponse
            {
                
            };

            return null;
        }
    }
}