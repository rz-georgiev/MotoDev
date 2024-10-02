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
        IUserService userService,
        MotoDevDbContext dbContext) : ICarRepairDetailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IUserService _userService = userService;

        public async Task<BaseResponse<IEnumerable<CarRepairDetailListingResponse>>> GetAllAsync()
        {
            var repairShops = _dbContext.RepairShops.Where(x => x.OwnerUserId == _userService.CurrentUserId)
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
            var repairDetail = request.ClientCarRepairDetailId > 0
                ? await _dbContext.ClientCarRepairsDetails.SingleOrDefaultAsync(x => x.Id == request.ClientCarRepairDetailId)
                : new ClientCarRepairDetail();

            repairDetail.ClientCarRepairId = request.ClientCarRepairId;
            repairDetail.RepairTypeId = request.RepairTypeId;
            repairDetail.RepairStatusId = request.RepairStatusId;
            repairDetail.Price = request.Price;

            if (request.ClientCarRepairDetailId > 0)
            {
                repairDetail.LastUpdatedAt = DateTime.UtcNow;
                repairDetail.LastUpdatedByUserId = _userService.CurrentUserId;

                _dbContext.Update(repairDetail);
            }
            else
            {
                repairDetail.RepairStatusId = (int)RepairStatusOption.ToDo;
                repairDetail.CreatedAt = DateTime.UtcNow;
                repairDetail.CreatedByUserId = _userService.CurrentUserId;
                repairDetail.IsActive = true;

                await _dbContext.AddAsync(repairDetail);
            }

            await _dbContext.SaveChangesAsync();


            var repair = await
                _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == repairDetail.ClientCarRepairId);

            var clientCar = await
                _dbContext.ClientCars.SingleOrDefaultAsync(x => x.Id == repair.ClientCarId);

            var client = await _dbContext.Clients.SingleOrDefaultAsync(x => x.Id == clientCar.ClientId);
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == client.UserId);
            var repairStatus = _dbContext.RepairStatuses.SingleOrDefault(x => x.Id == repairDetail.RepairStatusId);
            var repairType = _dbContext.RepairTypes.SingleOrDefault(x => x.Id == repairDetail.RepairTypeId);

            return new BaseResponse<CarRepairDetailListingResponse>
            {
                IsOk = true,
                Message = "",
                Result = new CarRepairDetailListingResponse
                {
                    ClientCarRepairDetailId = repairDetail.Id,
                    ClientName = $"{user.FirstName} {user.LastName}",
                    LicensePlateNumber = clientCar.LicensePlateNumber,
                    Price = repairDetail.Price,
                    RepairTypeName = repairType.Name,
                    Status = repairStatus.Name
                }
            };
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

        public async Task<BaseResponse<bool>> DeactivateByDetailIdAsync(int detailId)
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