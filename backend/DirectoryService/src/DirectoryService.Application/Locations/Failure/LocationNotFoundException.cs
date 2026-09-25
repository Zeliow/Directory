using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Locations.Failure;

public sealed class LocationNotFoundException : NotFoundException
{
    public LocationNotFoundException(Guid id)
        : base([Error.NotFound("location.not.found", $"Location with ID {id} not found.")])
    {
    }

    public LocationNotFoundException(IEnumerable<Guid> ids)
        : base([Error.NotFound("location.not.found", $"Locations not found: {string.Join(", ", ids)}")])
    {
    }
}
