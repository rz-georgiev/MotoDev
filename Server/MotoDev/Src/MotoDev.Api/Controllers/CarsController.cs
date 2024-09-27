using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarsController(IHttpContextAccessor accessor,
        ICarService carService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICarService _carService = carService;
        
        [HttpGet("GetAllCars")]
        public async Task<BaseResponse<IEnumerable<CarResponse>>> GetAllCars()
        {
            return await _carService.GetAllCarsAsync();
        }
    }
}