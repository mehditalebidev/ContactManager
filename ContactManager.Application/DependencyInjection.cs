using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ContactManager.Application.Common.Behaviors;
using FluentValidation;
using Mapster;
using MapsterMapper;

namespace ContactManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);

            services.AddScoped(
                typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            #region mapping
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            #endregion
            
            
            return services;
        }
    }
}
