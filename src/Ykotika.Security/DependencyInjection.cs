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
            services.AddScoped<IRefreshTokenHasher, RefreshTokenHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IEncryptor, Encryptor>();

            services.Configure<EncryptionOptions>(options =>
            configuration.GetSection(nameof(EncryptionOptions)).Bind(options));

            services.Configure<AccessTokenOptions>(options =>
            configuration.GetSection(nameof(AccessTokenOptions)).Bind(options));

            services.Configure<RefreshTokenOptions>(options =>
            configuration.GetSection(nameof(RefreshTokenOptions)).Bind(options));

            return services;
        }
    }
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public double ExpiresHours { get; set; }
    }

    public class AccessTokenOptions
    {
        public required JwtOptions JwtOptions { get; set; }
    }

    public class RefreshTokenOptions
    {
        public required JwtOptions JwtOptions { get; set; }
    }
}
