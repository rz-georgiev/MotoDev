using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId);

        Task<BaseResponse> DeactivateRepairUserByIdAsync(int id);

        Task<BaseResponse<UserResponse>> CreateAsync(UserRequest request);

    }
}