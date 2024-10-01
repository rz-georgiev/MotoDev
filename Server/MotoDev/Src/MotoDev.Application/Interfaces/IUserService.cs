using Microsoft.AspNetCore.Http;
using MotoDev.Common.Dtos;
using MotoDev.Domain.Entities;

namespace MotoDev.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId);

        Task<BaseResponse> DeactivateRepairUserByIdAsync(int id);

        Task<BaseResponse<UserResponse>> EditAsync(UserRequest request);

        Task<BaseResponse<UserResponse>> EditMinimizedAsync(UserMinimizedRequest request);

        Task<BaseResponse<UserExtendedResponse>> GetByIdAsync(int id);

        Task<BaseResponse<UserProfileImageUpdateResponse>> UpdateProfileImage(IFormFile file);

        Task<BaseResponse<IEnumerable<MechanicUserResponse>>> GetMechanicUsersAsync();
    }
}