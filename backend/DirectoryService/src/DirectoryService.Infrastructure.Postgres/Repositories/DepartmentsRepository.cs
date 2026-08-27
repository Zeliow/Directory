namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class DepartmentsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;

    public DepartmentsRepository(DirectoryServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //cap method to add a department entity to the database context
    public async Task AddDepartment(object entity)
    {
        await _dbContext.AddAsync(entity);
    }
}