using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Application.Services;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class RepairTypesController : ControllerBase
    {
        private readonly IRepairTypeService _repairTypeService;

        public RepairTypesController(IRepairTypeService repairTypeService)
        {
            _repairTypeService = repairTypeService;
        }

        [HttpGet("GetAll")]
        public async Task<BaseResponse<IEnumerable<RepairTypeResponse>>> GetAllAsync()
        {
            return await
                _repairTypeService.GetAllAsync();
        }
    }
}