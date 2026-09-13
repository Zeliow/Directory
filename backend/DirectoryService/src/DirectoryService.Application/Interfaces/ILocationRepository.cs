using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;

namespace DirectoryService.Application.Interfaces;

public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);

    Task<bool> IsUniqueLocationNameAsync(LocationName locationName, CancellationToken cancellationToken);
}