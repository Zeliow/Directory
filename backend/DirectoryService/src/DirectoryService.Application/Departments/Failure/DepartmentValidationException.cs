using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public sealed class DepartmentValidationException : BadRequestException
{
    public DepartmentValidationException(Error[] errors) : base(errors)
    {
    }
}