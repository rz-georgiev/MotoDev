using MotoDev.Core.Dtos;

namespace MotoDev.Services.Interaces
{
    public interface IEmailService
    {
        Task<BaseResponse> SendEmailAsync(string receipient, string message);
    }
}