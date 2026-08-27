namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class DepartmentsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public DepartmentsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddDepartment(Guid id)
    {
        await _dbContext.AddAsync(id);
    }
}