using System.Net;
using System.Net.Mail;
using Ykotika.Application.Interfaces;

namespace Ykotika.Security
{
    public class EmailVerifier : IEmailVerifier
    {
        private const string SUBJECT = "Подтверждение почты";
        private const string FROM_MAIL = "infinite.ellipse-for-dev@mail.ru";
        private const string PASSWORD = "uCenA7zM9pMJPNsiNULX";

        public async void SendVerificationLink(string userEmail, string link)
        {
            var message = $"Пожалуйста, подтвердите вашу почту, перейдя по ссылке: <a href='{link}'>Подтвердить</a>";

            using var smtpClient = new SmtpClient("smtp.mail.ru")
            {
                Port = 587,
                Credentials = new NetworkCredential(FROM_MAIL, PASSWORD),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(FROM_MAIL),
                Subject = SUBJECT,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(userEmail);
            Console.WriteLine(smtpClient.Credentials);
            Console.WriteLine(mailMessage.Body);
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
            }
        }
    }
}
