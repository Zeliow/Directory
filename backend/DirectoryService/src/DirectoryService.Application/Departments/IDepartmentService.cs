using DirectoryService.Contracts.Department;

namespace DirectoryService.Application.Departments;

public interface IDepartmentService
{
    Task<Guid> CreateAsync(CreateDepartmentDto departmentDto, CancellationToken cancellationToken);
}