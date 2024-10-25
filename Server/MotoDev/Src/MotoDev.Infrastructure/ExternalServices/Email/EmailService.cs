﻿using Microsoft.Extensions.Configuration;
using MotoDev.Common.Dtos;
using System.Net;
using System.Net.Mail;

namespace MotoDev.Infrastructure.ExternalServices.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<BaseResponse> SendEmailAsync(string recipient, string message)
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
                mailMessage.Subject = "MotoDev mail";
                mailMessage.Body = message;

                // Send the email.
                await smtpClient.SendMailAsync(mailMessage);

                return ResponseHelper.Success($"Email sent successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.Failure($"Email not sent successfully-> { ex.ToString()}");
            }
        }
    }
}