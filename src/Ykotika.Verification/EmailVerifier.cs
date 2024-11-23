using System.Net;
using System.Net.Mail;
using Ykotika.Application.Interfaces;

namespace Ykotika.Verification
{
    public class EmailVerifier : IEmailVerifier
    {
        private const string SUBJECT = "";

        public void SendVerificationLink(string userEmail, string link)
        {
            var message = $"Пожалуйста, подтвердите вашу почту, перейдя по ссылке: <a href='{link}'>Подтвердить</a>";

            using var smtpClient = new SmtpClient("\tsmtp.mail.ru")
            {
                Port = 465,
                Credentials = new NetworkCredential("infinite.ellipse-for-dev@mail.ru", "uCenA7zM9pMJPNsiNULX"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(userEmail),
                Subject = SUBJECT,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add("recipient@example.com");

            smtpClient.Send(mailMessage);
        }
    }
}
