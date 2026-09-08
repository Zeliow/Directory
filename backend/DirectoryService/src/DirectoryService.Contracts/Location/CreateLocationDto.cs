
using DirectoryService.Domain.LocationVO;

namespace DirectoryService.Contracts.Location;

sealed public record CreateLocationDto(AddressDto AddressDto, LocationName LocationName);

sealed public record AddressDto(string Country, string City, string Street);
