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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetById")]
        public async Task<BaseResponse<RoleResponse>> GetById(int id)
        {
            return await
                _roleService.GetByIdAsync(id);
        }

        [HttpGet("GetAll")]
        public async Task<BaseResponse<IEnumerable<RoleResponse>>> GetAll()
        {
            return await
                _roleService.GetAllAsync();
        }
    }
}