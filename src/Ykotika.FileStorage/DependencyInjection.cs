using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;

namespace Ykotika.FileStorage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFileStorage(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
