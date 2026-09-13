using DirectoryService.Application.Interfaces;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationsRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public LocationsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
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
            return !existingLocation;
        }
        catch (Exception ex)
        {
            throw new InvalidDataException("Operation fail: {0}", ex);
        }
    }
}