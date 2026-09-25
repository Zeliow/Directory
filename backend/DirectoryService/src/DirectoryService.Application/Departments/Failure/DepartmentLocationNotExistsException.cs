using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public sealed class DepartmentLocationAlreadyExistsException : AppException
{
    public DepartmentLocationAlreadyExistsException(Guid locationId)
        : base(Error.Conflict("department.location.already.exists", $"Location {locationId} is already linked to the department."))
    {
    }
}

public sealed class DepartmentLocationNotExistsException : NotFoundException
{
    public DepartmentLocationNotExistsException(Guid locationId)
        : base([Error.NotFound("department.location.not.found", $"Location {locationId} is not linked to the department.")])
    {
    }
}
