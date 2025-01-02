using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Ykotika.Application.Interfaces;

namespace Ykotika.Verification
{
    public class EmailVerifier : IEmailVerifier
    {
        private const string Subject = "Подтверждение почты ykotika.ru";
        private readonly EmailVerifierOptions _options;

        public EmailVerifier(IOptions<EmailVerifierOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        private string LoadTemplate(string path, Dictionary<string, string> placeholders)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"Шаблон не найден: {path}");

            string template = File.ReadAllText(path, Encoding.UTF8);
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

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "VerificationMessage.html");

            var placeholders = new Dictionary<string, string>
            {
                { "link", link }
            };

            string message;
            try
            {
                message = LoadTemplate(templatePath, placeholders);
                Console.WriteLine("Сообщение после замены плейсхолдеров:");
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки шаблона: {ex.Message}");
                throw;
            }

            using var smtpClient = new SmtpClient("smtp.mail.ru")
            {
                Port = 587,
                Credentials = new NetworkCredential(_options.Credentials.Address, _options.Credentials.Password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_options.Credentials.Address),
                Subject = Subject,
                IsBodyHtml = true
            };
            mailMessage.To.Add(userEmail);

            var htmlView = AlternateView.CreateAlternateViewFromString(message, null, MediaTypeNames.Text.Html);
            mailMessage.AlternateViews.Add(htmlView);
            Console.WriteLine("HTML-сообщение:");
            Console.WriteLine(htmlView.ContentStream);

            var plainTextView = AlternateView.CreateAlternateViewFromString(
                $"Пожалуйста, подтвердите вашу почту, перейдя по ссылке: {link}", null, MediaTypeNames.Text.Plain);
            mailMessage.AlternateViews.Add(plainTextView);

            Console.WriteLine("MailMessage.Subject: " + mailMessage.Subject);
            Console.WriteLine("MailMessage.To: " + string.Join(", ", mailMessage.To));
            Console.WriteLine("MailMessage.AlternateViews.Count: " + mailMessage.AlternateViews.Count);


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
        public required EmailCredentials Credentials { get; set; }
        public required string AesIv { get; set; }
    }

    public class EmailCredentials
    {
        public required string Address { get; set; }
        public required string Password { get; set; }
    }
}
