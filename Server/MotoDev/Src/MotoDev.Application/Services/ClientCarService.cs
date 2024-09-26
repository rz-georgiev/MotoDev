using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;
using MotoDev.Common.Enums;
using MotoDev.Infrastructure.Persistence;

namespace MotoDev.Application.Services
{
    public class ClientCarService(
        IHttpContextAccessor accessor,
        MotoDevDbContext dbContext) : IClientCarService
    {
        private readonly IHttpContextAccessor _accessor = accessor;

        private readonly MotoDevDbContext _dbContext = dbContext;

        public async Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCarsAsync(int clientId)
        {
            var data = await _dbContext.ClientCars.Where(x => x.ClientId == clientId)
                .Include(x => x.Car)
                .ThenInclude(x => x.Model)
                .ThenInclude(x => x.Brand)
                .ToListAsync();

            var result = data.Select(x => new ClientCarResponse
            {
                ClientCarId = x.Id,
                CarName = $"{x.Car.Brand.Name} {x.Car.Model.Name} {x.Car.Year} -> {x.LicensePlateNumber}"
            });

            return new BaseResponse<IEnumerable<ClientCarResponse>>
            {
                IsOk = true,
                Result = result
            };
        }
    }
}