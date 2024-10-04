using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Infrastructure.ExternalServices.Email;
using MotoDev.Infrastructure.Persistence;

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

            return ResponseHelper.Success<IEnumerable<CarResponse>>(cars);
        }
    }
}