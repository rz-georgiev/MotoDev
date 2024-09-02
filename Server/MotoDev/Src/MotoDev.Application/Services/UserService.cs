﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class UserService(IConfiguration configuration,
        IEmailService emailService,
        MotoDevDbContext dbContext) : IUserService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId)
        {
            var result = new List<UserResponse>();
            var repairShops = await _dbContext.RepairShops.Where(x => x.OwnerUserId == ownerUserId)
                .Include(x => x.RepairShopUsers.Where(s => s.IsActive))
                    .ThenInclude(x => x.User)
                        .ThenInclude(x => x.Role)
                .ToListAsync();

            foreach (var repairShop in repairShops)
            {
                foreach (var repairShopUser in repairShop.RepairShopUsers)
                {
                    result.Add(new UserResponse
                    {
                        Id = repairShopUser.Id,
                        FirstName = repairShopUser.User.FirstName,
                        LastName = repairShopUser.User.LastName,
                        Position = string.Join(", ", repairShopUser.User.Role.Name),
                        RepairShop = repairShop.Name
                    });
                }
            }

            return new BaseResponse<IEnumerable<UserResponse>> { Result = result };
        }

        public async Task<BaseResponse> DeactivateRepairUserByIdAsync(int id)
        {
            try
            {
                var repairUser = _dbContext.RepairShopUsers.SingleOrDefault(x => x.Id == id);
                repairUser!.IsActive = false;
                await _dbContext.SaveChangesAsync();

                return new BaseResponse
                {
                    IsOk = true,
                    Message = "Successfully deactivated record"
                };
            }
            catch (Exception)
            {
                return new BaseResponse
                {
                    IsOk = false,
                    Message = "An error occurred while trying to deactivate the record"
                };
            }
        }
    }
}