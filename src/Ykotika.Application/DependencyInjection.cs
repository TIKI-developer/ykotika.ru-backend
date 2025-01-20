using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ykotika.Application.Common.Behaviors;
using Ykotika.Application.Validation;

namespace Ykotika.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services
                .AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            var baseType = typeof(ValidationRules);
            var implementationTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract);

            foreach (var implementationType in implementationTypes)
            {
                services.AddTransient(baseType, implementationType);
                services.AddTransient(implementationType);
            }

            return services;
        }
    }
}
