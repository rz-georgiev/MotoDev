using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleResponse>> GetByIdAsync(int id);
            
        Task<BaseResponse<IEnumerable<RoleResponse>>> GetAllAsync();

    }
}