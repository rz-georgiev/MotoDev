using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MechanicRepairsController(IHttpContextAccessor accessor,
        IMechanicRepairService mechnanicRepairService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IMechanicRepairService _mechanicRepairService = mechnanicRepairService;

        [HttpGet("GetLastTenOrders")]
        public async Task<BaseResponse<IEnumerable<MechanicRepairResponse>>> GetLastTenOrders()
        {
            return await
                _mechanicRepairService.GetLastTenOrdersAsync();
        }

        [HttpPut("UpdateDetail")]
        public async Task<BaseResponse<bool>> UpdateDetail(MechanicDetailUpdateRequest request)
        {
            return await 
                _mechanicRepairService.UpdateDetailAsync(request);
        }

    }
}