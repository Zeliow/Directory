using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Department;
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

    public async Task AddAsync(Department department, CancellationToken cancellationToken)
    {
        var departmentExists = await _dbContext.Set<Department>().AnyAsync(l => l.Id == department.Id, cancellationToken);

        if (departmentExists)
        {
            throw new InvalidOperationException("Department with the same ID already exists.");
        }

        await _dbContext.AddAsync(department, cancellationToken);
        _logger.LogInformation("Department entity added to the database context: {DepartmentId}", department.Id);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<DepartmentPath?> GetPathByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
        return result?.Path;
    }

    public async Task<Department?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Set<Department>()
            .Include(d => d.Locations)
            .FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
        return department;
    }

    public async Task<IReadOnlyCollection<Department>> GetAllAsync(CancellationToken cancellationToken)
    {
        var departments = await _dbContext.Set<Department>().ToListAsync(cancellationToken);
        return departments;
    }

    public async Task<bool> UpdateNameAsync(Guid departmentId, DepartmentName departmentName, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
        if (department == null)
        {
            return false;
        }

        department.UpdateDepartmentName(departmentName);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var department = await _dbContext.Set<Department>().FirstOrDefaultAsync(d => d.Id == departmentId, cancellationToken);
        if (department == null)
        {
            return false;
        }
        else
        {
            _dbContext.Set<Department>().Remove(department);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}