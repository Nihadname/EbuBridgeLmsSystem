using EbuBridgeLmsSystem.Application.Interfaces;
using Resend;
using System.Net.Mail;
using System.Net;

namespace EbuBridgeLmsSystem.Infrastructure.Concretes
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body, bool isHtml = true)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("nihadcoding@gmail.com\r\n");
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("nihadcoding@gmail.com", "kixx kxou qgdj wgmx");
            smtpClient.Send(mailMessage);
        }
    }
}
