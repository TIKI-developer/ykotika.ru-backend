using Microsoft.Extensions.DependencyInjection;
using Ykotika.Application.Interfaces;
using Ykotika.Article;

namespace Ykotika.FileStorage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddArticle(this IServiceCollection services)
        {
            services.AddScoped<IArticleGenerator, ArticleGenerator>();

            return services;
        }
    }
}
