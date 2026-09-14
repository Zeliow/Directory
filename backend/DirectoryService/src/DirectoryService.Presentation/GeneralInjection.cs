using DirectoryService.Application;
using DirectoryService.Infrastructure.Postgres;

namespace DirectoryService.Presentation;

public static class GeneralInjection
{
    public static IServiceCollection AddProgrammDependencies(this IServiceCollection services)
    {
        services.AddApplicationDependencies();
        return services;
    }
}