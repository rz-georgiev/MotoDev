using MotoDev.Common.Dtos;

namespace MotoDev.Infrastructure.ExternalServices.Email
{
    public interface IEmailService
    {
        Task<BaseResponse> SendEmailAsync(string receipient, string message);
    }
}