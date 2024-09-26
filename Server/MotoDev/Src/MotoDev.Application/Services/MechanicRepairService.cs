using AutoMapper;
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
    public class MechanicRepairService(MotoDevDbContext dbContext,
        IMapper mapper,
        IHttpContextAccessor accessor) : IMechanicRepairService
    {
        private readonly MotoDevDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _accessor = accessor;

        public int GetLastTenRepairsAsync()
        {
            throw new NotImplementedException();
        }
    }
}