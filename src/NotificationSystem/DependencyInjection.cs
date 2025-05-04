using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;

namespace Ykotika.NotificationSystem
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationSystem(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NotificationSystemOptions>(options =>
            configuration.GetSection(nameof(NotificationSystemOptions)).Bind(options));

            services.AddScoped<INotificationMetadataProvider, NotificationMetadataProvider>();
            services.AddScoped<INotificationRedirectUriResolver, NotificationRedirectUriResolver>();
            services.AddScoped<NotificationService>();
            services.AddScoped<INotificationService>(sp => sp.GetRequiredService<NotificationService>());
            services.AddScoped<INotificationSender>(sp => sp.GetRequiredService<NotificationService>());


            return services;
        }
    }
}
