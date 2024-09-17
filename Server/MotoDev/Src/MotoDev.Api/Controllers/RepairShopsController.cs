using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        [HttpGet("GetById")]
        public async Task<BaseResponse<RepairShopResponse>> GetById(int id)
        {
            return await
                _repairShopService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetForSpecifiedOwner")]
        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwner(int ownerUserId)
        {
            return await
                _repairShopService.GetForSpecifiedOwnerAsync(ownerUserId);
        }

        [HttpGet("GetForSpecifiedIds")]
        public async Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedIds([FromQuery] IEnumerable<int> repairShopsIds)
        {
            return await
                _repairShopService.GetForSpecifiedIds(repairShopsIds);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("DeactivateById/{id}")]
        public async Task<BaseResponse<bool>> DeactivateById([FromRoute] int id)
        {
            return await
                _repairShopService.DeactivateByIdAsync(id);
        }
    }
}