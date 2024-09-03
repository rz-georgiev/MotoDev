using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRepairShopService
    {
        Task<BaseResponse<IEnumerable<RepairShopResponse>>> GetForSpecifiedOwner(int ownerUserId);

    }
}