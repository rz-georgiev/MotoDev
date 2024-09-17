using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;
using System.Collections.Generic;

namespace MotoDev.Application.Services
{
    public class RepairShopService(MotoDevDbContext dbContext, IMapper mapper) : IRepairShopService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedIds(IEnumerable<int> repairShopsIds)
        {
            try
            {
                var repairShops = await _dbContext.RepairShops.Where(x => repairShopsIds.Contains(x.Id))
                    .ToListAsync();

                var response = new BaseResponse<IEnumerable<RepairShopResponse>>
                {
                    IsOk = true,
                    Result = _mapper.Map<IEnumerable<RepairShopResponse>>(repairShops)
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

        public async Task<BaseResponse<RepairShopResponse>> GetByIdAsync(int id)
        {
            try
            {
                var repairShop = await _dbContext.RepairShops.SingleOrDefaultAsync(x =>
                x.Id == id);

                var response = new BaseResponse<RepairShopResponse>
                {
                    IsOk = true,
                    Result = new RepairShopResponse
                    {
                        Id = repairShop.Id,
                        Name = repairShop.Name,
                    }
                };

                return response;
            }
            catch (Exception)
            {
                return new BaseResponse<RepairShopResponse>
                {
                    IsOk = false,
                    Message = "An error occurred while fetching data"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwnerAsync(int ownerUserId)
        {
            try
            {
                var repairShops = await _dbContext.RepairShops
                .Where(x => x.OwnerUserId == ownerUserId && x.IsActive == true).ToListAsync();

                var response = new BaseResponse<IEnumerable<RepairShopResponse>>
                {
                    IsOk = true,
                    Result = repairShops.Select(x => new RepairShopResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Address = x.Address,
                        VatNumber = x.VatNumber,
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

        public async Task<BaseResponse<bool>> DeactivateByIdAsync(int id)
        {
            var repairShop = await _dbContext.RepairShops.SingleOrDefaultAsync(x => x.Id == id);
            if (repairShop == null)
            {
                return new BaseResponse<bool>
                {
                    IsOk = false,
                    Result = false
                };
            }

            repairShop.IsActive = false;
          
            _dbContext.Update(repairShop);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>
            {
                IsOk = true,
                Result = true
            };         
        }
    }
}