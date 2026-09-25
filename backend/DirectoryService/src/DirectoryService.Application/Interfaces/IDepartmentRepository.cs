using DirectoryService.Contracts.Department;
using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;

namespace DirectoryService.Application.Interfaces;

public interface IDepartmentRepository
{
    Task AddAsync(Department department, CancellationToken cancellationToken);

    Task<Department> UpdateNameAsync(Guid departmentId, CancellationToken cancellationToken);

    Task<DepartmentPath?> GetPathByIdAsync(Guid departmentId, CancellationToken cancellationToken);

    Task<Department?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid departmentId, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Department>> GetAllAsync(CancellationToken cancellationToken);
}