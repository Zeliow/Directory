using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public sealed class DepartmentParentIsNotExistException : NotFoundException
{
    public DepartmentParentIsNotExistException()
        : base([Error.NotFound("department.parent.not.found", "Parent department not found.")])
    {
    }
}
