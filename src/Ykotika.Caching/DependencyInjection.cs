using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ykotika.Caching
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                var connection = configuration.GetConnectionString("Redis");

                if (string.IsNullOrEmpty(connection))
                {
                    connection = Environment.GetEnvironmentVariable("Redis");
                }

                if (string.IsNullOrEmpty(connection))
                {
                    throw new InvalidOperationException("Не удалось найти строку подключения к серверу кеширования Redis. Укажите её в appsettings.json или в переменной окружения 'Redis'.");
                }

                options.Configuration = connection;
            });

            return services;
        }
    }
}
