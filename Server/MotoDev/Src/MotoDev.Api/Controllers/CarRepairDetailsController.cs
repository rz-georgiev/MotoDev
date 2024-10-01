using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarRepairDetailsController(IHttpContextAccessor accessor,
        ICarRepairDetailService carRepairService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly ICarRepairDetailService _carRepairDetailService = carRepairService;
        
        [HttpGet("GetAll")]
        public async Task<BaseResponse<IEnumerable<CarRepairDetailListingResponse>>> GetAll()
        {
            return await _carRepairDetailService.GetAllAsync();
        }

        [HttpGet("GetById")]
        public async Task<BaseResponse<CarRepairDetailEditDto>> GetById(int detailId)
        {
            return await _carRepairDetailService.GetByIdAsync(detailId);
        }

        [HttpPost("Edit")]
        public async Task<BaseResponse<CarRepairDetailListingResponse>> Edit([FromBody] CarRepairDetailEditDto request)
        {
            return await _carRepairDetailService.EditAsync(request);
        }


        [HttpPut("DeactivateByDetailId")]
        public async Task<BaseResponse<bool>> DeactivateByDetailId(int detailId)
        {
            return await _carRepairDetailService.DeactivateByDetailIdAsync(detailId);
        }

    }
}