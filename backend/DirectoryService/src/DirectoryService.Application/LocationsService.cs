using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application;

sealed public class LocationsService : ILocationsService
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger _logger;

    public LocationsService(ILocationRepository locationRepository, ILogger logger)
    {
        _locationRepository = locationRepository;
        _logger = logger;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var locationName = LocationName.Create(locationDto.LocationName);

        var locationAddress = Address.Create(
            locationDto.AddressDto.Country,
            locationDto.AddressDto.City,
            locationDto.AddressDto.Street);

        var location = Location.Create(locationName, locationAddress);
        _logger.LogInformation("Success create location with id{Id}", location.Id);

        var result = await _locationRepository.AddAsync(location, cancellationToken);

        return result;
    }
}