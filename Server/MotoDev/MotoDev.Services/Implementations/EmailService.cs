using Microsoft.Extensions.Configuration;
using MotoDev.Core.Dtos;
using MotoDev.Services.Interaces;
using System.Net;
using System.Net.Mail;

namespace MotoDev.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<BaseResponseModel> SendEmailAsync(string recipient, string message)
        {
            try
            {
                // Create a new SmtpClient object.
                var smtpClient = new SmtpClient();

                // Set the SMTP server address and port.
                smtpClient.Host = _configuration["EmailSender:Host"];
                smtpClient.Port = 8889;

                // Set the SMTP credentials.
                smtpClient.Credentials = new NetworkCredential(_configuration["EmailSender:From"], _configuration["EmailSender:Password"]);

                // Enable SSL.
                smtpClient.EnableSsl = false;

                // Create a new MailMessage object.
                var mailMessage = new MailMessage();

                // Set the sender and recipient addresses.
                mailMessage.From = new MailAddress(_configuration["EmailSender:From"]);
                mailMessage.To.Add(recipient);

                // Set the subject and body of the email.
                mailMessage.Subject = "EngineExpert mail";
                mailMessage.Body = message;

                // Send the email.
                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponseModel
                {
                    IsOk = true,
                    Message = "Email sent successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    IsOk = false,
                    Message = $"Email not sent successfully -> {ex.ToString()}"
                };
            }
        }
    }
}