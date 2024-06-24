using MotoDev.Core.Dtos;

namespace MotoDev.Services.Interaces
{
    public interface IEmailService
    {
        Task<BaseResponseModel> SendEmailAsync(string receipient, string message);
    }
}