using LibraryBook.Business.Dtos;
using LibraryBook.CrossCutting.Interfaces;
using LibraryBook.CrossCutting.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.CrossCutting.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IConfiguration _configuration;

        public EmailService(IOptions<EmailSettings> emailSettings,
                            IConfiguration configuration)
        {
            _emailSettings = emailSettings.Value;
            _configuration = configuration;
        }

        public Task SendConfirmEmailAsync(string email, string content)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailAsync(string email)
        {
            var emailRequest = new EmailDto();

            var content = BuildContentBody();

            emailRequest.Subject = _configuration.GetSection("EmailConfiguration:Subject").Value;
            emailRequest.ToEmail = email;
            emailRequest.Body = content;

            var mailMessage = new MailMessage();
            var smtp = new SmtpClient();

            mailMessage.From = new MailAddress(_configuration.GetSection("EmailConfiguration:FromEmail").Value, "");
            mailMessage.To.Add(new MailAddress(emailRequest.ToEmail));
            mailMessage.Subject = emailRequest.Subject;


            mailMessage.IsBodyHtml = true;
            mailMessage.Body = emailRequest.Body;
            smtp.Port = int.Parse(_configuration.GetSection("EmailConfiguration:Port").Value);
            smtp.Host = _configuration.GetSection("EmailConfiguration:Host").Value;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_configuration.GetSection("EmailConfiguration:Mail").Value, _configuration.GetSection("EmailConfiguration:Password").Value);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(mailMessage);
        }

        private string BuildContentBody()
        {
            return "teste";
        }

    }
}
