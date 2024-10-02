using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IMechanicRepairService
    {
        Task<BaseResponse<IEnumerable<MechanicRepairResponse>>> GetLastTenOrdersAsync();

        Task<BaseResponse<bool>> UpdateDetailAsync(MechanicDetailUpdateRequest request);
    }
}