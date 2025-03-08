using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;


namespace Ykotika.SpreadsheetService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSpreadsheet(this IServiceCollection services)
        {
            services.AddScoped<ISpreadsheetWorker, SpreadsheetWorker>();

            return services;
        }
    }
}
