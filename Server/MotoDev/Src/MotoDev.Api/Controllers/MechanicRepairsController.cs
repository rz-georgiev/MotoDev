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
        private readonly IMechanicRepairService _mechanicRepairServiec = mechnanicRepairService;
        
        [HttpGet("GetLastTenRepairs")]
        public async Task<BaseResponse<IEnumerable<ClientCarStatusResponse>>> GetLastTenRepairs()
        {
            return null;
        }

    }
}