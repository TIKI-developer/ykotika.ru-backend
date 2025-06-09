using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;

namespace Ykotika.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRINGS__DB_CONNECTION");
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Не удалось найти строку подключения. Укажите её в appsettings.json или в переменной окружения 'AppDbConnectionString'.");
            }

            services.AddDbContext<YkotikaDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<DbInitializer>();
            services.AddScoped<IYkotikaDbContext>(provider =>
                provider.GetService<YkotikaDbContext>());

            return services;
        }

    }
}
