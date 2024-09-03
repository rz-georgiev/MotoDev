using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IRoleService
    {
        Task<BaseResponse<IEnumerable<RoleResponse>>> GetAll();

    }
}