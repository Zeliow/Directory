using DirectoryService.Application.Interfaces;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationsRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    private readonly ILogger<LocationsRepository> _logger;

    public LocationsRepository(DirectoryServiceDbContext dbContext, ILogger<LocationsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(location, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return location.Id;
    }

    public async Task<bool> IsUniqueLocationNameAsync(LocationName locationName, CancellationToken cancellationToken)
    {
        var existingLocation = await _dbContext.Set<Location>().AnyAsync(l => l.Name == locationName, cancellationToken);
        _logger.LogInformation("Checking uniqueness of location name: {LocationName}, IsUnique: {IsUnique}", locationName, !existingLocation);
        return !existingLocation;
    }

    public async Task<bool> IsValidLocationsAsync(IEnumerable<Guid> locationIds, CancellationToken cancellationToken)
    {
        var distinctIds = locationIds.Distinct().ToList();

        var existingCount = await _dbContext.Set<Location>()
            .CountAsync(l => distinctIds.Contains(l.Id), cancellationToken);

        return existingCount == distinctIds.Count;
    }
}