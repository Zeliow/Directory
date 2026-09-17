using DirectoryService.Contracts.Department;
using DirectoryService.Domain;

namespace DirectoryService.Application.Departments;

public interface IDepartmentService
{
    Task<Guid> CreateAsync(CreateDepartmentDto departmentDto, CancellationToken cancellationToken);

    Task<Department> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Department>> GetDepartmentsAsync(CancellationToken cancellationToken);

    Task<bool> UpdateAsync(Guid departmentId, UpdateDepartmentDto departmentDto, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid DepartmentId, CancellationToken cancellationToken);

    Task<bool> CreateRelation(CreateDepartmentRelationDto departmentRelationDto, CancellationToken cancellationToken);

    Task<bool> DeleteRelation(DeleteDepartmentRelationDto departmentRelationDto, CancellationToken cancellationToken);
}