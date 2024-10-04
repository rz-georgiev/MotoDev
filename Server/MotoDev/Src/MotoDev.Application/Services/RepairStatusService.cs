using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class RepairStatusService(
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IRepairStatusService
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<RepairStatusResponse>>> GetAllAsync()
        {
            var result = await _dbContext.RepairStatuses.Select(x => new RepairStatusResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return ResponseHelper.Success<IEnumerable<RepairStatusResponse>>(result);
        }
    }
}