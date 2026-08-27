namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class PositionsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public PositionsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Cap method to add a position entity to the database context
    public async Task AddPosition(object entity)
    {
        await _dbContext.AddAsync(entity);
    }
}