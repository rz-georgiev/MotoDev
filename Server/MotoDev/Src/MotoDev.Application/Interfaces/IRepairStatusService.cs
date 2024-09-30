using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRepairStatusService
    {
        Task<BaseResponse<IEnumerable<RepairStatusResponse>>> GetAllAsync();

    }
}