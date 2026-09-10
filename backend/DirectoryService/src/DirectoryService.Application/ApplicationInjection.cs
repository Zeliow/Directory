using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ApplicationInjection).Assembly);
        services.AddScoped<ILocationsService, LocationsService>();
        return services;
    }
}