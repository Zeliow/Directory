using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public sealed class DepartmentNotExistException : NotFoundException
{
    public DepartmentNotExistException(Guid id) : base([Error.NotFound("department.not.found", $"Department with ID {id} not found")])
    {
    }
}