using DirectoryService.Application.Interfaces;
using DirectoryService.Domain;

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
        return location.Id;
    }

    // Cap method to add a location entity to the database context
    public async Task AddLocation(object entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public async Task<bool> IsUniqueLocationNameAsync(string locationName, CancellationToken cancellationToken)
    {
        //cap of DB
        return true;
    }
}