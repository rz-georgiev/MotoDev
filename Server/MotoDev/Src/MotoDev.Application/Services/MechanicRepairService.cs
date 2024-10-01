using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class MechanicRepairService(MotoDevDbContext dbContext,
        IMapper mapper,
        IHttpContextAccessor accessor) : IMechanicRepairService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _accessor = accessor;

        public async Task<BaseResponse<IEnumerable<MechanicRepairResponse>>> GetLastTenOrdersAsync()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId")!.Value);
            
            var result = await _dbContext.ClientCarRepairs.Where(x => x.PerformedByMechanicUserId == userId)
                .Select(repair => new MechanicRepairResponse
                {
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

            return new BaseResponse<IEnumerable<MechanicRepairResponse>>
            {
                IsOk = true,
                Result = result,
            };
        }
    }
}