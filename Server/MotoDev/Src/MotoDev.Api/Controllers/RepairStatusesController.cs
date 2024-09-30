using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RepairStatusesController : ControllerBase
    {
        private readonly IRepairStatusService _repairStatusService;

        public RepairStatusesController(IRepairStatusService repairStatusService)
        {
            _repairStatusService = repairStatusService;
        }

        [HttpGet("GetAll")]
        public async Task<BaseResponse<IEnumerable<RepairStatusResponse>>> GetAllAsync()
        {
            return await
                _repairStatusService.GetAllAsync();
        }
    }
}