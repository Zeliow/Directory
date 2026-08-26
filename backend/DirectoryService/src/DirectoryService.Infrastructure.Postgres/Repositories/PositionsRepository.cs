namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class PositionsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public PositionsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddPosition(Guid id)
    {
        await _dbContext.AddAsync(id);
    }
}