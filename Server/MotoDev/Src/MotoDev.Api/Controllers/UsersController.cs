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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllForCurrentOwnerUserId")]
        [Authorize(Roles = "Owner")]
        public async Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId)
        {
            return await
                _userService.GetAllForCurrentOwnerUserIdAsync(ownerUserId);
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("DeactivateRepairUserById")]
        public async Task<BaseResponse> DeactivateRepairUserById(int id)
        {
            return await
                _userService.DeactivateRepairUserByIdAsync(id);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("Edit")]
        public async Task<BaseResponse<UserResponse>> Edit([FromBody]UserRequest request)
        {      
            return await
                _userService.EditAsync(request);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetById")]
        public async Task<BaseResponse<UserExtendedResponse>> GetById(int id)
        {
            return await
                _userService.GetByIdAsync(id);
        }

        [HttpPost("UpdateProfileImage")]
        public async Task<BaseResponse<UserProfileImageUpdateResponse>> UpdateProfileImage(IFormFile file)
        {
            return await
                _userService.UpdateProfileImage(file);
        }
    }
}