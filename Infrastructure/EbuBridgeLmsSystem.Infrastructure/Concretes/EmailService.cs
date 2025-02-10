using EbuBridgeLmsSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Infrastructure.Concretes
{
    public class EmailService : IEmailService
    {
        private readonly IResend _resend;


        public EmailService(IResend resend)
        {
            _resend = resend;
        }
        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            var message = new EmailMessage();
            message.From = "nihadcoding@gmail.com";
            message.To.Add(to);
            message.Subject =subject;
            message.HtmlBody = body;

            await _resend.EmailSendAsync(message);
        }
    }
}
