using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public class DepartmentParentIsNotExistException : BadRequestException
{
    public DepartmentParentIsNotExistException() : base([Errors.DepartmentErrors.ParentNotFound()])
    {
    }
}