using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Microsoft.Extensions.Options;
using Ykotika.Application.Interfaces;

namespace Ykotika.Email
{
    public class EmailService(IOptions<EmailVerifierOptions> options) : IEmailService
    {
        private readonly EmailVerifierOptions _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        private readonly TemplateLoader _templateLoader = new(new() {
            {"Verification", "Templates.VerificationMessage.html"}
        });

        public string GetStringTemplateByName(string name, Dictionary<string, string> placeholders)
        {
            return _templateLoader.Templates.First(e => e.Name == name).Get(placeholders);
        }

        public async Task Send(string toAddress, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(toAddress)) throw new ArgumentException("Email не должен быть пустым.", nameof(toAddress));

            using var smtpClient = new SmtpClient(_options.Host)
            {
                Port = _options.Port,
                Credentials = new NetworkCredential(_options.Credentials.Address, _options.Credentials.Password),
                EnableSsl = _options.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_options.Credentials.Address),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toAddress);

            var htmlView = AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html);
            mailMessage.AlternateViews.Add(htmlView);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP ошибка: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка: {ex.Message}");
                throw;
            }
        }
    }
}
