using ContactManager.Application.Common.Interfaces.Repositories;
using ContactManager.Application.Common.Interfaces.Services;
using ContactManager.Infrastructure.Context;
using ContactManager.Infrastructure.Repositories;
using ContactManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Microsoft.Extensions.Configuration.ConfigurationManager configuration)
    {
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ContactManagerDbContext>(options =>
            options.UseNpgsql(connectionString!));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return services;
    }
}
