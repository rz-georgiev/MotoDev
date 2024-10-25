﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.Persistence;
using MotoDev.Infrastructure.Persistence.Migrations;
using System.Collections.Generic;

namespace MotoDev.Application.Services
{
    public class RepairShopService(MotoDevDbContext dbContext,
        IMapper mapper,
        IUserService userService,
        IHttpContextAccessor accessor) : IRepairShopService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IUserService _userService = userService;
        private readonly IHttpContextAccessor _accessor = accessor;

        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedIds(IEnumerable<int> repairShopsIds)
        {
            try
            {
                var repairShops = await _dbContext.RepairShops.Where(x => repairShopsIds.Contains(x.Id))
                    .ToListAsync();

                return ResponseHelper.Success(_mapper.Map<IEnumerable<RepairShopResponse>>(repairShops));
            }
            catch (Exception)
            {
                return ResponseHelper.Failure<IEnumerable<RepairShopResponse>>(new List<RepairShopResponse> { }, "An error occurred while fetching data");
            }
        }

        public async Task<BaseResponse<RepairShopResponse>> GetByIdAsync(int id)
        {
            try
            {
                var repairShop = await _dbContext.RepairShops.SingleOrDefaultAsync(x =>
                x.Id == id);

                var result = _mapper.Map<RepairShopResponse>(repairShop);
                return ResponseHelper.Success(result);
            }
            catch (Exception)
            {
                return ResponseHelper.Failure<RepairShopResponse>(new RepairShopResponse { }, "An error occurred while fetching data");
            }
        }

        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwnerAsync(int ownerUserId)
        {
            try
            {
                var repairShops = await _dbContext.RepairShops
                .Where(x => x.OwnerUserId == ownerUserId && x.IsActive == true).ToListAsync();

                var result = repairShops.Select(x => new RepairShopResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    VatNumber = x.VatNumber,
                }).ToList();

                return ResponseHelper.Success<IEnumerable<RepairShopResponse>>(result);

            }
            catch (Exception)
            {
                return ResponseHelper.Failure<IEnumerable<RepairShopResponse>>(new List<RepairShopResponse> { },
                    $"An error occurred while fetching data");
            }
        }

        public async Task<BaseResponse<RepairShopResponse>> EditAsync(RepairShopRequest request)
        {
            var newRepairShop = new RepairShop();

            if (request.Id > 0)
            {
                var repairShop = await _dbContext.RepairShops.SingleOrDefaultAsync(x => x.Id == request.Id);

                repairShop.Name = request.Name;
                repairShop.Email = request.Email;
                repairShop.City = request.City;
                repairShop.VatNumber = request.VatNumber;
                repairShop.PhoneNumber = request.PhoneNumber;
                repairShop.LastUpdatedAt = DateTime.UtcNow;
                repairShop.LastUpdatedByUserId = _userService.CurrentUserId;

                _dbContext.Update(repairShop);
            }
            else
            {
                newRepairShop = _mapper.Map<RepairShop>(request);
                newRepairShop.OwnerUserId = _userService.CurrentUserId;
                newRepairShop.CreatedByUserId = _userService.CurrentUserId;
                newRepairShop.IsActive = true;

                await _dbContext.AddAsync(newRepairShop);
            }

            await _dbContext.SaveChangesAsync();

            // Add current owner to the new repair shop
            var repairShopUser = new RepairShopUser
            {
                RepairShopId = newRepairShop.Id,
                UserId = _userService.CurrentUserId,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = _userService.CurrentUserId,
            };

            await _dbContext.AddAsync(repairShopUser);
            await _dbContext.SaveChangesAsync();

            request.Id = request.Id == 0
                ? newRepairShop.Id :
                request.Id;

            return ResponseHelper.Success(_mapper.Map<RepairShopResponse>(request));

        }

        public async Task<BaseResponse<bool>> DeactivateByIdAsync(int id)
        {
            var repairShop = await _dbContext.RepairShops.SingleOrDefaultAsync(x => x.Id == id);
            if (repairShop == null)
            {
                return ResponseHelper.Failure(false);
            }

            repairShop.IsActive = false;

            _dbContext.Update(repairShop);
            await _dbContext.SaveChangesAsync();

            return ResponseHelper.Success(true);

        }
    }
}