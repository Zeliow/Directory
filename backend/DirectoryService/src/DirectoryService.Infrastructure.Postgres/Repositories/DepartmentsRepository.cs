using DirectoryService.Application.Interfaces;
using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class DepartmentsRepository : IDepartmentRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    private readonly ILogger<DepartmentsRepository> _logger;

    public DepartmentsRepository(DirectoryServiceDbContext dbContext, ILogger<DepartmentsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //cap method to add a department entity to the database context
    public async Task AddAsync(Department department, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(department, cancellationToken);
        _logger.LogInformation("Department entity added to the database context: {DepartmentId}", department.Id);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<DepartmentPath?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
        return result?.Path;
    }
}