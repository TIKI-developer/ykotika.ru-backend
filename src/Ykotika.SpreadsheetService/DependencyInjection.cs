using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;
using Ykotika.SpreadsheetService;


namespace Ykotika.Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSpreadsheet(this IServiceCollection services)
        {
            services.AddScoped<ISpreadsheetService, SpreadsheetGenerator>();
           
            return services;
        }
    }
}
