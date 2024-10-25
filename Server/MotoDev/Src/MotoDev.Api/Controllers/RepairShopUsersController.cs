using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RepairShopUsersController : ControllerBase
    {
        private readonly IRepairShopUserService _repairShopUserService;
        
        public RepairShopUsersController(IRepairShopUserService repairShopUserService)
        {
            _repairShopUserService = repairShopUserService;
        }

        
        [HttpGet("GetRepairShopsForCurrentUser")]
        public async Task<BaseResponse<IEnumerable<RepairShopUserResponse>>> GetRepairShopsForCurrentUser()
        {
            return await
                _repairShopUserService.GetRepairShopsForCurrentUser();
        }

        [HttpGet("GetById")]
        public async Task<BaseResponse<RepairShopUserResponse>> GetById(int id)
        {
            return await
                _repairShopUserService.GetByIdAsync(id);
        }
    }
}