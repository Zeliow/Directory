namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public LocationsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Cap method to add a location entity to the database context
    public async Task AddLocation(object entity)
    {
        await _dbContext.AddAsync(entity);
    }
}