using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface ICarRepairDetailService
    {
        Task<BaseResponse<IEnumerable<CarRepairDetailListingResponse>>> GetAllAsync();

        Task<BaseResponse<bool>> DeactivateByDetailIdAsync(int detailId);       

        Task<BaseResponse<CarRepairDetailListingResponse>> EditAsync(CarRepairDetailEditDto request);

        Task<BaseResponse<CarRepairDetailEditDto>> GetByIdAsync(int detailId);
    }
}