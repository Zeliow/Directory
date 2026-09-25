using DirectoryService.Application.Exceptions;
using DirectoryService.Application.Helpers;
using DirectoryService.Application.Interfaces;
using DirectoryService.Application.Locations.Failure;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Locations;

sealed public class LocationsService : ILocationsService
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<LocationsService> _logger;
    private readonly IValidator<CreateLocationDto> _createLocationValidator;

    public LocationsService(ILocationRepository locationRepository, ILogger<LocationsService> logger, IValidator<CreateLocationDto> createLocationValidator)
    {
        _locationRepository = locationRepository;
        _logger = logger;
        _createLocationValidator = createLocationValidator;
    }

    public async Task<Guid> CreateAsync(CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createLocationValidator.ValidateAsync(locationDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Invalid location data provided.");
            throw new BadRequestException(validationResult.ToErrors());
        }

        var locationName = LocationName.Create(locationDto.LocationName);

        var isUnique = await _locationRepository.IsUniqueLocationNameAsync(Guid.Empty, locationName, cancellationToken);

        if (!isUnique)
        {
            _logger.LogError("Location with the same name already exists.");
            throw new LocationNameDuplicateException(locationName.Value);
        }

        var locationAddress = Address.Create(
            locationDto.AddressDto.Country,
            locationDto.AddressDto.City,
            locationDto.AddressDto.Street);

        var location = Location.Create(locationName, locationAddress);
        _logger.LogInformation("Success create location with id{Id}", location.Id);

        var result = await _locationRepository.AddAsync(location, cancellationToken);

        return result;
    }

    public async Task<bool> UpdateAsync(Guid locationId, UpdateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var locationName = LocationName.Create(locationDto.LocationName);
        var isUnique = await _locationRepository.IsUniqueLocationNameAsync(locationId, locationName, cancellationToken);
        if (!isUnique)
        {
            _logger.LogError("Location with the same name already exists.");
            throw new LocationNameDuplicateException(locationName.Value);
        }

        var locationAddress = Address.Create(
            locationDto.AddressDto.Country,
            locationDto.AddressDto.City,
            locationDto.AddressDto.Street);

        var result = await _locationRepository.UpdateDataAsync(locationId, locationAddress, locationName, cancellationToken);
        return result;
    }

    public async Task<IReadOnlyCollection<Location>> ListLocationsAsync(CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetAllAsync(cancellationToken);
        return location;
    }

    public async Task<Location> GetByidAsync(Guid locationId, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetByIdAsync(locationId, cancellationToken);
        if (location == null)
        {
            _logger.LogError("Location with id {LocationId} not found.", locationId);
            throw new LocationNotFoundException(locationId);
        }
        return location;
    }

    public async Task<bool> DeleteAsync(Guid locationId, CancellationToken cancellationToken)
    {
        var result = await _locationRepository.DeleteAsync(locationId, cancellationToken);
        _logger.LogInformation("Deleting location with id {LocationId}", locationId);
        return result;
    }
}
