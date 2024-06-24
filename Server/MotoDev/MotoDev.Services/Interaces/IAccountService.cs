using MotoDev.Core.Dtos;

namespace MotoDev.Services.Interaces
{
    public interface IAccountService
    {
        Task<BaseResponseModel> LoginAsync(LoginRequest request);

        Task<BaseResponseModel> RegisterAsync(RegisterAccountRequest request);

        Task<BaseResponseModel> ConfirmAccountAsync(ConfirmAccountRequest request);

        Task<BaseResponseModel> ForgottenPasswordAsync(ForgottenEmailRequest request);

        Task<BaseResponseModel> ResetPasswordAsync(ResetPasswordRequest request);
    }
}