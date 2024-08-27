using MotoDev.Core.Dtos;

namespace MotoDev.Services.Interaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<UserResponse>>> GetAllForCurrentOwnerUserIdAsync(int ownerUserId);
    }
}