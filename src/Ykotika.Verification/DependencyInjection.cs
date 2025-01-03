using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;

namespace Ykotika.Verification
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddVerification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailVerifier, EmailVerifier>();
            services.Configure<EmailVerifierOptions>(options =>
            configuration.GetSection(nameof(EmailVerifierOptions)).Bind(options));

            return services;
        }
    }
}
