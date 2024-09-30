using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRepairTypeService
    {
        Task<BaseResponse<IEnumerable<RepairTypeResponse>>> GetAllAsync();
    }
}