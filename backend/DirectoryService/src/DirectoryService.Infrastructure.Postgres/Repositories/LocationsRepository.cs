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
        try
        {
            await _dbContext.AddAsync(location, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return location.Id;
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Operation fail: {0}", ex);
        }
    }

    // Cap method to add a location entity to the database context
    public async Task AddLocation(object entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public async Task<bool> IsUniqueLocationNameAsync(LocationName locationName, CancellationToken cancellationToken)
    {
        try
        {
            var existingLocation = await _dbContext.Set<Location>().AnyAsync(l => l.Name == locationName, cancellationToken);
            _logger.LogInformation("Checking uniqueness of location name: {LocationName}, IsUnique: {IsUnique}", locationName, !existingLocation);
            return !existingLocation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking uniqueness of location name: {LocationName}", locationName);
            throw new InvalidDataException("Operation fail: {0}", ex);
        }
    }
}