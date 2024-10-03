﻿using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Infrastructure.Persistence;
using System.Collections.Generic;

namespace MotoDev.Application.Services
{
    public class RoleService(MotoDevDbContext dbContext) : IRoleService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<RoleResponse>> GetByIdAsync(int id)
        {
            try
            {
                var role = await _dbContext.Roles.SingleOrDefaultAsync(x =>
                x.Id == id);

                var response = new BaseResponse<RoleResponse>
                {
                    IsOk = true,
                    Result = new RoleResponse
                    {
                        Id = role.Id,
                        Name = role.Name,
                    }
                };

                return response;
            }
            catch (Exception)
            {
                return ResponseHelper.Failure(new RoleResponse { }, "An error occurred while fetching data");
            }
        }

        public async Task<BaseResponse<IEnumerable<RoleResponse>>> GetAllAsync()
        {
            try
            {
                var roles = await _dbContext.Roles.Where(x =>
                x.Id == (int)RoleOption.Client ||
                x.Id == (int)RoleOption.Mechanic)
                    .ToListAsync();

                var response = new BaseResponse<IEnumerable<RoleResponse>>
                {
                    IsOk = true,
                    Result = roles.Select(x => new RoleResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList()
                };

                return response;
            }
            catch (Exception)
            {
                return ResponseHelper.Failure<IEnumerable<RoleResponse>> (new List<RoleResponse> { },
                    "An error occurred while fetching data");

            }
        }
    }
}