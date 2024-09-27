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
    public class ClientCarsController(IHttpContextAccessor accessor,
        IClientCarService clientCarService) : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IClientCarService _clientCarService = clientCarService;

        [HttpGet("GetAllClientCars")]
        public async Task<BaseResponse<IEnumerable<ClientCarListingReponse>>> GetAllClientCars()
        {
            return await _clientCarService.GetAllClientCarsAsync();
        }

        [HttpPut("DeactivateByClientCarId")]
        public async Task<BaseResponse<bool>> DeactivateByClientCarId(int clientCarId)
        {
            return await _clientCarService.DeactivateByCarRepairIdAsync(clientCarId);
        }

        [HttpGet("GetById")]
        public async Task<BaseResponse<ClientCarEditDto>> GetById(int clientCarId)
        {
            return await _clientCarService.GetByIdAsync(clientCarId);
        }

        [HttpPost("Edit")]
        public async Task<BaseResponse<ClientCarListingReponse>> Edit([FromBody] ClientCarEditDto request)
        {
            return await _clientCarService.EditAsync(request);
        }

        [HttpGet("GetClientCars")]
        public async Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCars(int clientId)
        {
            return await _clientCarService.GetClientCarsAsync(clientId);
        }
    }
}