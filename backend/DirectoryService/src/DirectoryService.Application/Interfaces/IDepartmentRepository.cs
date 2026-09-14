using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;

namespace DirectoryService.Application.Interfaces;

public interface IDepartmentRepository
{
    Task AddAsync(Department department, CancellationToken cancellationToken);

    Task<DepartmentPath?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken);
}