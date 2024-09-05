using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRepairShopUserService
    {
        Task<BaseResponse<RepairShopUserResponse>> GetByIdAsync(int id);

    }
}