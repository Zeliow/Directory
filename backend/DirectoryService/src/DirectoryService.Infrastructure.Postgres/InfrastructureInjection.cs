// Infrastructure.Postgres/InfrastructureInjection.cs
using DirectoryService.Application.Interfaces;
using DirectoryService.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Infrastructure.Postgres;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["DataAccess:Provider"] ?? "EfCore";

        if (string.Equals(provider, "Dapper", StringComparison.OrdinalIgnoreCase))
            services.AddScoped<ILocationRepository, DapperLocationsRepository>();
        else
            services.AddScoped<ILocationRepository, LocationsRepository>();

        services.AddScoped<IDepartmentRepository, DepartmentsRepository>();
        return services;
    }
}