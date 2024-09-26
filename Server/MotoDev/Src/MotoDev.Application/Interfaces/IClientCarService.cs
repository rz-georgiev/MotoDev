using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IClientCarService
    {
        Task<BaseResponse<IEnumerable<ClientCarResponse>>> GetClientCarsAsync(int clientId);

    }
}