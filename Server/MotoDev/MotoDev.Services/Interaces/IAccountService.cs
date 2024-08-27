﻿using MotoDev.Core.Dtos;

namespace MotoDev.Services.Interaces
{
    public interface IAccountService
    {
        Task<BaseResponse> LoginAsync(LoginRequest request);

        Task<BaseResponse> RegisterAsync(RegisterAccountRequest request);

        Task<BaseResponse> ConfirmAccountAsync(ConfirmAccountRequest request);

        Task<BaseResponse> ForgottenPasswordAsync(ForgottenEmailRequest request);

        Task<BaseResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}