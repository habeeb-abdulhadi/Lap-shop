using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace LapShop.Models
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fMail = "abujad602@gmail.com";
            var fPassword = "yemvpnmstvlortcs";
            //var theMsg = new MailMessage();
            //theMsg.From = new MailAddress(fMail);
            //theMsg.Subject = subject;
            //theMsg.To.Add(email);
            //theMsg.Body = $"<html><body>{htmlMessage}</body></html>";
            //theMsg.IsBodyHtml = true;

            //var smtpClint = new SmtpClient("smtp.gmail.com")
            //{
            //    UseDefaultCredentials = true,
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential(fMail,fPassword),
            //    Port= 587
            //};
            //smtpClint.Send(theMsg);
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(fMail);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = $"<html><body>{htmlMessage}</body></html>";

            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(fMail, fPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
