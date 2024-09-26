using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface ICarRepairService
    {
        Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarRepairsAsync();
    }
}