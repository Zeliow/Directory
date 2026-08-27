namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public LocationsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddLocation(Guid id)
    {
        await _dbContext.AddAsync(id);
    }
}