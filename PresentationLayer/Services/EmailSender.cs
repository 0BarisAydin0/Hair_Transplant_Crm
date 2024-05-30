using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PresentationLayer.Controllers;
using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly ILogger<HomeController> _logger;
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {

            _smtpSettings = smtpSettings.Value;

        }
        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Connect(_smtpSettings.SmtpServer, _smtpSettings.Port, _smtpSettings.UseSsl);
            smtpClient.Authenticate(_smtpSettings.Username, _smtpSettings.Password);

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_smtpSettings.DisplayName, _smtpSettings.Username));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = body
            };

            smtpClient.Send(emailMessage);
            smtpClient.Disconnect(true);

            return Task.CompletedTask;
        }
    }
}
