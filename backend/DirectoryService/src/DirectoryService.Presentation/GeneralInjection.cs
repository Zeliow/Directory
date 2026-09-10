using DirectoryService.Application;

namespace DirectoryService.Presentation;

public static class GeneralInjection
{
    public static IServiceCollection AddProgrammDependencies(this IServiceCollection services)
    {
        services.AddApplicationDependencies();

        return services;
    }
}