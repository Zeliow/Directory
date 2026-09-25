using DirectoryService.Shared;

namespace DirectoryService.Application.Locations.Failure;

public sealed class LocationNameDuplicateException : AppException
{
    public LocationNameDuplicateException(string name)
        : base(Error.Conflict("location.name.duplicate", $"Location with name '{name}' already exists."))
    {
    }
}
