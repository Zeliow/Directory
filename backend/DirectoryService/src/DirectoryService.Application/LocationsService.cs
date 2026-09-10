using DirectoryService.Application.Interfaces;
using DirectoryService.Contracts.Location;
using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application;

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

        if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }

        var isUnique = await _locationRepository.IsUniqueLocationNameAsync(locationDto.LocationName, cancellationToken);

        if (!isUnique) { throw new InvalidOperationException("Location with the same name already exists"); }

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