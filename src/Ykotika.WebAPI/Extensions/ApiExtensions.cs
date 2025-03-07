using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ykotika.Domain.Entities;
using Ykotika.Security;
using Ykotika.WebAPI.Authorization;
using Ykotika.WebAPI.Authorization.Requirements;
using Ykotika.WebAPI.Constants;

namespace Ykotika.WebApi.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var accessTokenOptions = provider.GetRequiredService<IOptions<AccessTokenOptions>>().Value;

            if (string.IsNullOrEmpty(accessTokenOptions.JwtOptions.SecretKey))
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
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenOptions.JwtOptions.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[Cookies.ACCESS_TOKEN_NAME];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.CONTENT_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentRequirement
                    ([UserRole.Admin,
                      UserRole.Director]));
                });

                options.AddPolicy(Policies.POST_PRODUCT_COMMENT_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentRequirement
                    ([UserRole.Admin,
                      UserRole.Director,
                      UserRole.Moderator,
                      UserRole.Author],
                      checkPublished: false));
                });

                options.AddPolicy(Policies.READ_AGREEMENT_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentRequirement
                    ([UserRole.Admin], checkPublished: false));
                });

                options.AddPolicy(Policies.CATEGORY_LIST_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentListRequirement
                    ([UserRole.Admin,
                      UserRole.Director]));
                });
                options.AddPolicy(Policies.FORM_LIST_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentListRequirement
                    ([UserRole.Admin,
                      UserRole.Director]));
                });
                options.AddPolicy(Policies.PRODUCT_TYPE_LIST_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentListRequirement
                    ([UserRole.Admin,
                      UserRole.Director]));
                });
                options.AddPolicy(Policies.PRODUCT_LIST_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentListRequirement
                    ([UserRole.Moderator,
                      UserRole.Admin,
                      UserRole.Director]));
                });
                options.AddPolicy(Policies.PRODUCT_DUPLICATE_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ContentRequirement([UserRole.Moderator,
                      UserRole.Admin,
                      UserRole.Director], checkPublished: false));
                });
                options.AddPolicy(Policies.PRODUCT_STATUS_POLICY, policy =>
                {
                    policy
                    .AddRequirements
                    (new ProductStatusRequirement());
                });
            });
            services.AddSingleton<IAuthorizationHandler, AuthorHandler>();
            services.AddSingleton<IAuthorizationHandler, ProductStatusHandler>();
            services.AddSingleton<IAuthorizationHandler, PublishedHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton<IAuthorizationHandler, ContentListHandler>();
        }
    }
}
