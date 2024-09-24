using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientsController(IHttpContextAccessor accessor,
        IClientService clientService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IClientService _clientService = clientService;
        
        [HttpGet("GetMyCarsStatuses")]
        public async Task<BaseResponse<IEnumerable<ClientCarStatusResponse>>> GetMyCarsStatuses()
        {
            return await _clientService.GetMyCarsStatusesAsync();
        }

    }
}