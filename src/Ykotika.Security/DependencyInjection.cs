using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;


namespace Ykotika.Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IEmailVerifier, EmailVerifier>();

            var jwtOptionsSection = configuration.GetSection(nameof(JwtOptions));
            var secretKey = jwtOptionsSection.GetValue<string>("SecretKey")
                            ?? Environment.GetEnvironmentVariable("JwtOptions_SecretKey");

            int expiresHours = jwtOptionsSection.GetValue<int?>("ExpiresHours")
                               ?? (int.TryParse(Environment.GetEnvironmentVariable("JwtOptions_ExpiresHours"), out var parsedHours) ? parsedHours : 24);

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("SecretKey для JWT не найден. Укажите его в appsettings.json или переменной окружения 'JwtOptions_SecretKey'.");
            }

            services.Configure<JwtOptions>(options =>
            {
                options.SecretKey = secretKey;
                options.ExpiresHours = expiresHours;
            });

            return services;
        }
    }
}
