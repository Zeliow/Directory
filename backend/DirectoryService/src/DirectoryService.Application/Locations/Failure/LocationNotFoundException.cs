using DirectoryService.Application.Exceptions;
using DirectoryService.Shared;

namespace DirectoryService.Application.Locations.Failure;

public sealed class LocationNotFoundException : NotFoundException
{
    public LocationNotFoundException(Guid id)
        : base([Error.NotFound("location.not.found", $"Локация с ID {id} не найдена.")])
    {
    }

    public LocationNotFoundException(IEnumerable<Guid> ids)
        : base([Error.NotFound("location.not.found", $"Локации не найдены: {string.Join(", ", ids)}")])
    {
    }
}
