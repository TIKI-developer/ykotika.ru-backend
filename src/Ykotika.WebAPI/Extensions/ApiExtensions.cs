using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ykotika.Security;

namespace Ykotika.WebApi.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var jwtOptions = provider.GetRequiredService<IOptions<JwtOptions>>().Value;

            if (string.IsNullOrEmpty(jwtOptions.SecretKey))
            {
                throw new InvalidOperationException("SecretKey для JWT не найден.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["creeper"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireGuestRole", policy => policy.RequireRole("Guest"));
                options.AddPolicy("RequireDefaultRole", policy => policy.RequireRole("Default"));
            });
        }
    }
}
