using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ykotika.Security;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

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
                            context.Token = context.Request.Cookies["accessToken"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireGuestRole", policy => policy.RequireRole(Roles.GUEST_ROLE));
                options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole(Roles.CUSTOMER_ROLE));
                options.AddPolicy("RequireAuthorRole", policy => policy.RequireRole(Roles.AUTHOR_ROLE));
                options.AddPolicy("RequireModeratorRole", policy => policy.RequireRole(Roles.MODERATOR_ROLE));
                options.AddPolicy("RequireDirectorRole", policy => policy.RequireRole(Roles.DIRECTOR_ROLE));
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(Roles.ADMIN_ROLE));
                options.AddPolicy("ProductListGuard", policy =>
                policy.RequireAssertion(context =>
                {
                    var filter = context.Resource as ProductFilterDto;

                    return true;
                }));
            });
        }
    }
}
