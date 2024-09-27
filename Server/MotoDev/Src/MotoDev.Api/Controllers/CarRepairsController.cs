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

        [HttpPut("DeactivateByCarRepairId")]
        public async Task<BaseResponse<bool>> DeactivateByCarRepairId(int carRepairId)
        {
            return await _carRepairService.DeactivateByCarRepairIdAsync(carRepairId);
        }


        [HttpGet("GetById")]
        public async Task<BaseResponse<CarRepairEditResponse>> GetById(int carRepairId)
        {
            return await _carRepairService.GetByIdAsync(carRepairId);
        }

        [HttpPost("Edit")]
        public async Task<BaseResponse<CarRepairResponse>> Edit([FromBody] CarRepairRequest request)
        {
            return await _carRepairService.EditAsync(request);
        }
    }
}