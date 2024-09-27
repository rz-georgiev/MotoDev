using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface ICarService
    {
        Task<BaseResponse<IEnumerable<CarResponse>>> GetAllCarsAsync();

    }
}