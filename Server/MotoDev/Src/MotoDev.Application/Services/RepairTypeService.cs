using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class RepairTypeService(
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IRepairTypeService
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<RepairTypeResponse>>> GetAllAsync()
        {
            var result = await _dbContext.RepairTypes.Select(x => new RepairTypeResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return new BaseResponse<IEnumerable<RepairTypeResponse>>
            {
                IsOk = true,
                Result = result,
            };
        }
    }
}