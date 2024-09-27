using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface ICarRepairService
    {
        Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarsRepairsAsync();

        Task<BaseResponse<CarRepairResponse>> EditAsync(CarRepairRequest request);

        Task<BaseResponse<CarRepairEditResponse>> GetByIdAsync(int carRepairId);
    }
}