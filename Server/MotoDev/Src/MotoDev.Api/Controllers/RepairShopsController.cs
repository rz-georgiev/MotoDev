using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RepairShopsController : ControllerBase
    {
        private readonly IRepairShopService _repairShopService;
        
        public RepairShopsController(IRepairShopService repairShopService)
        {
            _repairShopService = repairShopService;
        }

        [HttpGet("GetForSpecifiedOwner")]
        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwner(int ownerUserId)
        {
            return await
                _repairShopService.GetForSpecifiedOwner(ownerUserId);
        }
    }
}