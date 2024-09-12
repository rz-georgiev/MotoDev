using Microsoft.AspNetCore.Http;
using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId);

        Task<BaseResponse> DeactivateRepairUserByIdAsync(int id);

        Task<BaseResponse<UserResponse>> EditAsync(UserRequest request);

        Task<BaseResponse<UserExtendedResponse>> GetByIdAsync(int id);

        Task<BaseResponse<string>> UpdateProfileImage(IFormFile file);

    }
}