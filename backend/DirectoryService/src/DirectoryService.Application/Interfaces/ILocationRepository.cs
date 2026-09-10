using DirectoryService.Domain;

namespace DirectoryService.Application.Interfaces;

public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);

    Task<bool> IsUniqueLocationNameAsync(string locationName, CancellationToken cancellationToken);
}