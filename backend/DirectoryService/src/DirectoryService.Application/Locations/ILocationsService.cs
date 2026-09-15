using DirectoryService.Contracts.Location;
using DirectoryService.Domain;

namespace DirectoryService.Application.Locations;

public interface ILocationsService
{
    Task<Guid> CreateAsync(CreateLocationDto locationDto, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(Guid locationId, UpdateLocationDto locationDto, CancellationToken cancellationToken);>

    Task<IReadOnlyCollection<Location>> ListLocationsAsync(CancellationToken cancellationToken);
}