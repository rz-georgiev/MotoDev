using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarRepairsController(IHttpContextAccessor accessor,
        ICarRepairService carRepairService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICarRepairService _carRepairService = carRepairService;
        
        [HttpGet("GetAllCarsRepairs")]
        public async Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarsRepairs()
        {
            return await _carRepairService.GetAllCarsRepairsAsync();
        }

    }
}