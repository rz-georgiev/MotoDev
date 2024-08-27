using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoDev.Core.Dtos;
using MotoDev.Services.Interaces;

namespace MotoDev.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId)
        {
            return await
                _userService.GetAllForCurrentOwnerUserIdAsync(ownerUserId);
        }
    }
}