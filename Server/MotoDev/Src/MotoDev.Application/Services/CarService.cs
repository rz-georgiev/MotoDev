using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Constants;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Common.Extensions;
using MotoDev.Domain.Entities;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;
using MotoDev.Infrastructure.Persistence.Migrations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Application.Services
{
    public class CarService(IConfiguration configuration,
        IEmailService emailService,
        IHttpContextAccessor accessor,
        ICloudinaryService cloudinaryService,
        MotoDevDbContext dbContext) : ICarService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailService _emailService = emailService;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService;
        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<CarResponse>>> GetAllAsync()
        {
            var cars = await _dbContext.Cars
                .Include(x => x.Brand)
                .Include(x => x.Model)
                .Select(x => new CarResponse
                {
                    CarId = x.Id,
                    CarName = $"{x.Brand.Name} {x.Model.Name} {x.Year}",
                }).ToListAsync();

            return new BaseResponse<IEnumerable<CarResponse>>
            {
                IsOk = true,
                Result = cars
            };
        }
    }
}