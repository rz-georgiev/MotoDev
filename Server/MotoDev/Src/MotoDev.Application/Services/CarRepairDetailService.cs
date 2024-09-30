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
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Application.Services
{
    public class CarRepairDetailService(IConfiguration configuration,
        IEmailService emailService,
        IHttpContextAccessor accessor,
        ICloudinaryService cloudinaryService,
        MotoDevDbContext dbContext) : ICarRepairDetailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<CarRepairDetailListingResponse>>> GetAllAsync()
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
                .Include(x => x.ClientCars)
                    .ThenInclude(x => x.ClientCarRepairs)
                    .ThenInclude(x => x.ClientCarRepairsDetails
                        .Where(x => x.IsActive))
                    .ThenInclude(x => x.RepairType)
                     .Include(x => x.ClientCars)
                    .ThenInclude(x => x.ClientCarRepairs)
                    .ThenInclude(x => x.ClientCarRepairsDetails)
                    .ThenInclude(x => x.RepairStatus)
                .Include(x => x.User).ToListAsync();

            var repairsDetails = clients
                .SelectMany(x => x.ClientCars
                .SelectMany(x => x.ClientCarRepairs
                .SelectMany(x => x.ClientCarRepairsDetails)));

            var result = repairsDetails.Select(x => new CarRepairDetailListingResponse
            {
                ClientCarRepairDetailId = x.Id,
                ClientName = $"{x.ClientCarRepair.ClientCar.Client.User.FirstName}" +
                $" {x.ClientCarRepair.ClientCar.Client.User.LastName}",
                LicensePlateNumber = x.ClientCarRepair.ClientCar.LicensePlateNumber,
                Price = x.Price,
                RepairTypeName = x.RepairType.Name,
                Status = x.RepairStatus.Name
            });


            return new BaseResponse<IEnumerable<CarRepairDetailListingResponse>>
            {
                IsOk = true, 
                Result = result
            };
        }

        public async Task<BaseResponse<CarRepairDetailListingResponse>> EditAsync(CarRepairDetailEditDto request)
        {

            //var currentClientCarRepair = request.CarRepairId > 0
            //    ? await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == request.CarRepairId)
            //    : new ClientCarRepair();

            //int userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);

            //currentClientCarRepair.ClientCarId = request.ClientCarId;

            //if (request.CarRepairId > 0)
            //{
            //    currentClientCarRepair.LastUpdatedAt = DateTime.UtcNow;
            //    currentClientCarRepair.LastUpdatedByUserId = userId;

            //    _dbContext.Update(currentClientCarRepair);
            //}
            //else
            //{
            //    currentClientCarRepair.RepairStatusId = (int)RepairStatusOption.ToDo;
            //    currentClientCarRepair.CreatedAt = DateTime.UtcNow;
            //    currentClientCarRepair.CreatedByUserId = userId;

            //    await _dbContext.AddAsync(currentClientCarRepair);
            //}

            //await _dbContext.SaveChangesAsync();

            //var clientCar = await
            //    _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == currentClientCarRepair.ClientCarId);

            //var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == clientCar.ClientId);
            //var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == client.UserId);
            //var repairStatus = _dbContext.RepairStatuses.SingleOrDefault(x => x.Id == currentClientCarRepair.RepairStatusId);

            //return new BaseResponse<CarRepairResponse>
            //{
            //    IsOk = true,
            //    Message = "",
            //    Result = new CarRepairResponse
            //    {
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        LicensePlateNumber = clientCar.LicensePlateNumber,
            //        CarRepairId = currentClientCarRepair.Id,
            //        Status = repairStatus.Name,
            //        StatusId = repairStatus.Id,
            //        RepairDateTime = currentClientCarRepair.CreatedAt,
            //    }
            //};

            return null;
        }

        public async Task<BaseResponse<CarRepairDetailEditDto>> GetByIdAsync(int detailId)
        {
            var detail = await _dbContext.ClientCarRepairsDetails.SingleOrDefaultAsync(x => x.Id == detailId);

            return new BaseResponse<CarRepairDetailEditDto>
            {
                IsOk = true,
                Result = new CarRepairDetailEditDto
                {
                    ClientCarRepairDetailId = detail.Id,
                    ClientCarRepairId = detail.ClientCarRepairId,
                    Price = detail.Price,
                    RepairStatusId = detail.RepairStatusId,
                    RepairTypeId = detail.RepairTypeId,
                }
            };
        }

        public async Task<BaseResponse<bool>> DeactivateByDetailId(int detailId)
        {
            var detail = await _dbContext.ClientCarRepairsDetails.SingleOrDefaultAsync(x => x.Id == detailId);
            detail.IsActive = false;
            
            _dbContext.Update(detail);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>
            {
                IsOk = true,
                Result = true,
            };
        }
    }
}