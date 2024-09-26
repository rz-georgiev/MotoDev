using MotoDev.Common.Dtos;
using MotoDev.Domain.Entities;

namespace MotoDev.Application.Interfaces
{
    public interface IClientService
    {
        Task<BaseResponse<IEnumerable<ClientCarStatusResponse>>> GetMyCarsStatusesAsync();

        Task<BaseResponse<IEnumerable<ClientResponse>>> GetAllClientsAsync();
    }
}