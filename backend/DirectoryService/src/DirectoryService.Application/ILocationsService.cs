using DirectoryService.Contracts.Location;

namespace DirectoryService.Application;

public interface ILocationsService
{
    Task<Guid> CreateAsync(CreateLocationDto locationDto, CancellationToken cancellationToken);
}