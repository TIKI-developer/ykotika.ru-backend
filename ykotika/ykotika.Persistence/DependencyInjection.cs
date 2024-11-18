using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ykotika.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ykotika.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppDbConnectionString");

            services.AddDbContext<YkotikaDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IYkotikaDbContext>(provider =>
                provider.GetService<YkotikaDbContext>());

            return services;
        }
    }
}
