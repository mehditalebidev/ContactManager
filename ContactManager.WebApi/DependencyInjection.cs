using ContactManager.WebApi.Common.Errors;
using ContactManager.WebApi.Common.Mapping;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using ContactManager.WebApi.Common.Behaviors;
using MediatR;

namespace ContactManager.WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            
            
            
            services.AddMappings();

            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, ContactManagerProblemDetailsFactory>();
            services.AddSwaggerGen();

            return services;

        }
        
        
    }
}
