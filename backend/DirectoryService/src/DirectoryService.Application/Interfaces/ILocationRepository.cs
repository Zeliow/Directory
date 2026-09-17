using DirectoryService.Contracts.Location;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;

namespace DirectoryService.Application.Interfaces;

public interface ILocationRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);

    Task<bool> IsUniqueLocationNameAsync(LocationName locationName, CancellationToken cancellationToken);

    Task<bool> UpdateDataAsync(Guid locationId, Address address, LocationName locationName, CancellationToken cancellationToken);

    Task<bool> IsValidLocationsAsync(IEnumerable<Guid> locationIds, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Location>> GetAllAsync(CancellationToken cancellationToken);

    Task<Location?> GetByIdAsync(Guid locationId, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid locationId, CancellationToken cancellationToken);
}