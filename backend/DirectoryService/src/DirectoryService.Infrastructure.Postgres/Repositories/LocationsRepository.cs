using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Location;
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
        var locationExists = await _dbContext.Set<Location>().AnyAsync(l => l.Id == location.Id, cancellationToken);

        if (locationExists)
        {
            throw new InvalidOperationException("Location with the same ID already exists.");
        }

        await _dbContext.AddAsync(location, cancellationToken);
        _logger.LogInformation("Location entity added to the database context: {LocationId}", location.Id);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return location.Id;
    }

    public async Task<IReadOnlyCollection<Location>> GetAllAsync(CancellationToken cancellationToken)
    {
        var locations = await _dbContext.Set<Location>().ToListAsync(cancellationToken);
        return locations;
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

    public async Task<bool> UpdateDataAsync(Guid locationId, Address address, LocationName locationName, CancellationToken cancellationToken)
    {
        var location = _dbContext.Set<Location>().FirstOrDefault(l => l.Id == locationId);
        if (location == null)
        {
            return false;
        }

        location.UpdateData(locationName, address);

        return true;
    }
}