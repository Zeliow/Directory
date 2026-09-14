using DirectoryService.Contracts.Department;

namespace DirectoryService.Application;

public interface IDepartmentsService
{
    Task<Guid> CreateAsync(CreateDepartmentDto departmentDto, CancellationToken cancellationToken);
}