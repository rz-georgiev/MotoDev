﻿using MotoDev.Common.Dtos;

namespace MotoDev.Application.Interfaces
{
    public interface ICarRepairService
    {
        Task<BaseResponse<IEnumerable<CarRepairResponse>>> GetAllCarsRepairsAsync();

        Task<BaseResponse<bool>> DeactivateByCarRepairIdAsync(int carRepairId);       

        Task<BaseResponse<CarRepairResponse>> EditAsync(CarRepairRequest request);

        Task<BaseResponse<CarRepairEditResponse>> GetByIdAsync(int carRepairId);

        Task<BaseResponse<IEnumerable<CarRepairSelectResponse>>> GetClientsRepairsAsync();
    }
}