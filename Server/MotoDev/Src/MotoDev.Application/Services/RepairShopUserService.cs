using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.Persistence;
using System.Collections.Generic;

namespace MotoDev.Application.Services
{
    public class RepairShopUserService(MotoDevDbContext dbContext,
        IMapper mapper,
        IHttpContextAccessor accessor) : IRepairShopUserService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _accessor = accessor;

        public async Task<BaseResponse<IEnumerable<RepairShopUserResponse>>> GetRepairShopsForCurrentUser()
        {
            var userId = Convert.ToInt32(_accessor.HttpContext.User.FindFirst("userId").Value);
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