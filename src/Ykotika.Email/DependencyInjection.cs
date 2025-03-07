using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;

namespace Ykotika.Email
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<EmailVerifierOptions>(options =>
            configuration.GetSection(nameof(EmailVerifierOptions)).Bind(options));

            return services;
        }
    }
}
