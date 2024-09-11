using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRepairShopService
    {
        Task<BaseResponse<RepairShopResponse>> GetByIdAsync(int id);

        Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwnerAsync(int ownerUserId);

        Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedIds(IEnumerable<int> repairShopsIds);

    }
}