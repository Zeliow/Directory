using Dapper;
using DirectoryService.Application.Interfaces;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public sealed class DapperLocationsRepository : ILocationRepository
{
    private readonly string _connectionString;
    private readonly ILogger<DapperLocationsRepository> _logger;

    public DapperLocationsRepository(IConfiguration configuration, ILogger<DapperLocationsRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        _logger = logger;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO public.locations ("Id", name, address, created_at, updated_at)
            VALUES (@Id, @Name, @Address, @CreatedAt, @UpdatedAt);
            """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await connection.ExecuteAsync(new CommandDefinition(sql, new
        {
            id = location.Id,
            Name = location.Name.Value,
            Address = location.Address.Value,
            location.CreatedAt,
            location.UpdatedAt,
        }, cancellationToken: cancellationToken));

        _logger.LogInformation("Location {Id} inserted via Dapper", location.Id);
        return location.Id;
    }

    public async Task<bool> IsUniqueLocationNameAsync(LocationName locationName, CancellationToken cancellationToken)
    {
        const string sql = "SELECT EXISTS (SELECT 1 FROM locations WHERE name = @Name);";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var exists = await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sql, new { Name = locationName.Value }, cancellationToken: cancellationToken));

        return !exists;
    }

    public async Task<bool> IsValidLocationsAsync(IEnumerable<Guid> locationIds, CancellationToken cancellationToken)
    {
        var distinctIds = locationIds?.Distinct().ToArray() ?? Array.Empty<Guid>();
        if (distinctIds.Length == 0)
        {
            return false;
        }

        const string sql = """
        SELECT COUNT(*)
        FROM public.locations
        WHERE "Id" = ANY(@Ids);
        """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var command = new CommandDefinition(
            sql,
            new { Ids = distinctIds },
            cancellationToken: cancellationToken);

        var count = await connection.ExecuteScalarAsync<int>(command);

        return count == distinctIds.Length;
    }
}