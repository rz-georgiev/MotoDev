using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class RoleService(MotoDevDbContext dbContext) : IRoleService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<RoleResponse>>> GetAll()
        {
            try
            {
                var repairShops = await _dbContext.Roles.Where(x =>
                x.Id == (int)RoleOption.Administrator ||
                x.Id == (int)RoleOption.Mechanic)
                    .ToListAsync();

                var response = new BaseResponse<IEnumerable<RoleResponse>>
                {
                    IsOk = true,
                    Result = repairShops.Select(x => new RoleResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList()
                };

                return response;
            }
            catch (Exception)
            {
                return new BaseResponse<IEnumerable<RoleResponse>>
                {
                    IsOk = false,
                    Message = "An error occurred while fetching data"
                };
            }
        }
    }
}