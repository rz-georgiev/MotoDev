using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Infrastructure.Persistence;
using System.Runtime.CompilerServices;

namespace MotoDev.Application.Services
{
    public class MechanicRepairService(MotoDevDbContext dbContext,
        IMapper mapper,
        IHttpContextAccessor accessor,
        ICloudinaryService cloudinaryService,
        IUserService _userService) : IMechanicRepairService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly IUserService userService = _userService;

        public async Task<BaseResponse<IEnumerable<MechanicRepairResponse>>> GetLastTenOrdersAsync()
        {
            var result = await _dbContext.ClientCarRepairs.Where(x =>
                    x.PerformedByMechanicUserId == _userService.CurrentUserId &&
                    x.IsActive)
                .OrderByDescending(x => x.Id)
                .Take(10)
                .Select(repair => new MechanicRepairResponse
                {
                    CarImageUrl = _cloudinaryService.GetImageUrlById(repair.ClientCar.Car.ImageId),
                    CarDescription = repair.ClientCar.Car.Description,
                    OrderName = $"#{repair.Id} / {repair.ClientCar.Client.User.FirstName} " +
                    $"{repair.ClientCar.Client.User.LastName} / {repair.ClientCar.Car.Brand.Name} " +
                    $"{repair.ClientCar.Car.Model.Name} " +
                    $"{repair.ClientCar.Car.Year} / " +
                    $"{repair.ClientCar.LicensePlateNumber}",
                    Details = repair.ClientCarRepairsDetails
                        .Where(x => x.IsActive)
                        .Select(detail => new MechanicRepairResponseDetail
                        {
                            RepairDetailId = detail.Id,
                            Notes = detail.Notes,
                            RepairName = detail.RepairType.Name,
                            StatusId = detail.RepairStatusId,
                        })
                })
                .Where(x => x.Details.Any())
                .ToListAsync();

            return ResponseHelper.Success<IEnumerable<MechanicRepairResponse>>(result);

        }

        public async Task<BaseResponse<bool>> UpdateDetailAsync(MechanicDetailUpdateRequest request)
        {
            var detail = await _dbContext.ClientCarRepairsDetails.SingleOrDefaultAsync(x => x.Id == request.RepairDetailId);

            detail.Notes = request.NewNotes;
            detail.RepairStatusId = request.NewStatusId;

            detail.LastUpdatedAt = DateTime.UtcNow;
            detail.LastUpdatedByUserId = _userService.CurrentUserId;

            if (request.NewStatusId == (int)RepairStatusOption.InProgress)
            {
                detail.RepairStartDateTime = DateTime.UtcNow;
                detail.RepairEndDateTime = null;
            }
            else if (request.NewStatusId == (int)RepairStatusOption.Done)
            {
                detail.RepairEndDateTime = DateTime.UtcNow;
            }
            else
            {
                detail.RepairStartDateTime = null;
                detail.RepairEndDateTime = null;
            }


            _dbContext.Update(detail);
            await _dbContext.SaveChangesAsync();

            var repair = await _dbContext.ClientCarRepairs.SingleOrDefaultAsync(x => x.Id == detail.ClientCarRepairId);
            var allDetails = await _dbContext.ClientCarRepairsDetails.Where(x => x.ClientCarRepairId == repair.Id).ToListAsync();

            if (allDetails.Where(x => x.RepairStatusId == (int)RepairStatusOption.ToDo).Count() == allDetails.Count())
                repair.RepairStatusId = (int)RepairStatusOption.ToDo;
            else if (allDetails.Where(x => x.RepairStatusId == (int)RepairStatusOption.Done).Count() == allDetails.Count())
                repair.RepairStatusId = (int)RepairStatusOption.Done;
            else
                repair.RepairStatusId = (int)RepairStatusOption.InProgress;

            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Success(true);

        }
    }
}