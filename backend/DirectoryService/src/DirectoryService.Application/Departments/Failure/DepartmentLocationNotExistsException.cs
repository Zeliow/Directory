using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Departments.Failure;

public sealed class DepartmentLocationAlreadyExistsException : AppException
{
    public DepartmentLocationAlreadyExistsException(Guid locationId)
        : base(Error.Conflict("department.location.already.exists", $"Локация {locationId} уже привязана к отделу."))
    {
    }
}

public sealed class DepartmentLocationNotExistsException : NotFoundException
{
    public DepartmentLocationNotExistsException(Guid locationId)
        : base([Error.NotFound("department.location.not.found", $"Локация {locationId} не привязана к отделу.")])
    {
    }
}
