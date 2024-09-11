using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;
using System.Collections.Generic;

namespace MotoDev.Application.Services
{
    public class RepairShopUserService(MotoDevDbContext dbContext,
        IMapper mapper) : IRepairShopUserService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<IEnumerable<RepairShopUserResponse>>> GetRepairShopsForUserId(int userId)
        {
            var repairShopUsers = await _dbContext.RepairShopUsers.Where(x => x.UserId == userId)
                .ToListAsync();
            
            return new BaseResponse<IEnumerable<RepairShopUserResponse>>
            {
                Result = _mapper.Map<IEnumerable<RepairShopUserResponse>>(repairShopUsers)
            };
        }

        public async Task<BaseResponse<RepairShopUserResponse>> GetByIdAsync(int id)
        {
            var repairShopUser = await _dbContext.RepairShopUsers.SingleOrDefaultAsync(x => x.Id == id);
            return new BaseResponse<RepairShopUserResponse>
            {
                Result = _mapper.Map<RepairShopUserResponse>(repairShopUser)
            };
        }
    }
}