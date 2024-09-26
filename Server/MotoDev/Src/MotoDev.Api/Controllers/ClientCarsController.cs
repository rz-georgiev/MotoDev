using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Application.Interfaces;
using MotoDev.Common.Dtos;

namespace MotoDev.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientCarsController(IHttpContextAccessor accessor,
        IClientCarService clientCarService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IClientCarService _clientCarService = clientCarService;
        
        [HttpGet("GetClientCars")]
        public async Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCars(int clientId)
        {
            return await _clientCarService.GetClientCarsAsync(clientId);
        }
    }
}