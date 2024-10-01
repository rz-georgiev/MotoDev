using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardController(IDashboardService dashboardService) : ControllerBase
    {
        private readonly IDashboardService _dashboardService = dashboardService;

        [HttpGet("GetDashboardData")]
        [Authorize(Roles = "Owner")]
        public async Task<BaseResponse<DashboardResponse>> GetDashboardData()
        {
            return await
                _dashboardService.GetDashboardDataAsync();
        }
    }
}