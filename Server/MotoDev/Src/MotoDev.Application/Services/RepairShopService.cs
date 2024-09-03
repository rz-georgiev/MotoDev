using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class RepairShopService(MotoDevDbContext dbContext) : IRepairShopService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwner(int ownerUserId)
        {
            try
            {
                var repairShops = await _dbContext.RepairShops
                .Where(x => x.OwnerUserId == ownerUserId).ToListAsync();


                var response = new BaseResponse<IEnumerable<RepairShopResponse>>
                {
                    IsOk = true,
                    Result = repairShops.Select(x => new RepairShopResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList()
                };

                return response;
            }
            catch (Exception)
            {
                return new BaseResponse<IEnumerable<RepairShopResponse>>
                {
                    IsOk = false,
                    Message = "An error occurred while fetching data"
                };
            }
        }
    }
}