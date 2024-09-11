using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRepairShopUserService
    {
        Task<BaseResponse<IEnumerable<RepairShopUserResponse>>> GetRepairShopsForUserId(int userId);

        Task<BaseResponse<RepairShopUserResponse>> GetByIdAsync(int id);

    }
}