using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IClientCarService
    {
        Task<BaseResponse<IEnumerable<ClientCarListingReponse>>> GetAllClientCarsAsync();

        Task<BaseResponse<ClientCarEditDto>> GetByIdAsync(int clientCarId);

        Task<BaseResponse<ClientCarListingReponse>> EditAsync(ClientCarEditDto request);

        Task<BaseResponse<bool>> DeactivateByCarRepairIdAsync(int clientCarId);

        Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCarsAsync(int clientId);
    }
}