using DirectoryService.Domain;

namespace DirectoryService.Application.Interfaces;

public interface IDepartmentRepository
{
    Task AddAsync(Department department, CancellationToken cancellationToken);)
}