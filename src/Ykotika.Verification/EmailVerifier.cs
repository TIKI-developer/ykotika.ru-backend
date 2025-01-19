using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Ykotika.Application.Interfaces;

namespace Ykotika.Verification
{
    public class EmailVerifier(IOptions<EmailVerifierOptions> options) : IEmailVerifier
    {
        private const string Subject = "Подтверждение почты ykotika.ru";
        private readonly EmailVerifierOptions _options = options.Value ?? throw new ArgumentNullException(nameof(options));

        private static string LoadTemplate(string resourceName, Dictionary<string, string> placeholders)
        {
            var assembly = typeof(EmailVerifier).Assembly;

            var fullResourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))
                ?? throw new FileNotFoundException($"Ресурс с именем {resourceName} не найден в сборке.");

            using var stream = assembly.GetManifestResourceStream(fullResourceName)
            ?? throw new InvalidOperationException($"Не удалось получить поток для ресурса {fullResourceName}.");

            using var reader = new StreamReader(stream, Encoding.UTF8);
            string template = reader.ReadToEnd();

            foreach (var placeholder in placeholders)
            {
                template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
            }

            return template;
        }

        public async Task SendVerificationLinkAsync(string userEmail, string link)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentException("Email не должен быть пустым.", nameof(userEmail));
            if (string.IsNullOrWhiteSpace(link)) throw new ArgumentException("Ссылка не должна быть пустой.", nameof(link));

            var placeholders = new Dictionary<string, string>
            {
                { "link", link }
            };

            string message;
            try
            {
                message = LoadTemplate("Templates.VerificationMessage.html", placeholders);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки шаблона: {ex.Message}");
                throw;
            }


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
                Subject = Subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(userEmail);

            var htmlView = AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html);
            mailMessage.AlternateViews.Add(htmlView);

            var plainTextView = AlternateView.CreateAlternateViewFromString(
                $"Пожалуйста, подтвердите вашу почту, перейдя по ссылке: {link}", null, MediaTypeNames.Text.Plain);

            mailMessage.AlternateViews.Add(plainTextView);

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

    public class EmailVerifierOptions
    {
        public required string Host { get; set; }
        public required int Port { get; set; }
        public required bool EnableSsl { get; set; }
        public required EmailCredentials Credentials { get; set; }
        public required string AesIv { get; set; }
    }

    public class EmailCredentials
    {
        public required string Address { get; set; }
        public required string Password { get; set; }
    }
}
