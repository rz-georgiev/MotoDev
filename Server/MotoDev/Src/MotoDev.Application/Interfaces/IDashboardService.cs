using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<BaseResponse<DashboardResponse>> GetDashboardDataAsync();
    }
}