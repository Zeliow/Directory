using DirectoryService.Contracts.Department;
using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;

namespace DirectoryService.Application.Interfaces;

public interface IDepartmentRepository
{
    Task AddAsync(Department department, CancellationToken cancellationToken);

    Task<bool> UpdateNameAsync(Guid departmentId, DepartmentName departmentName, CancellationToken cancellationToken);

    Task<DepartmentPath?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Department>> GetAllAsync(CancellationToken cancellationToken);
}