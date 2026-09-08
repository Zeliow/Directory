namespace DirectoryService.Contracts.Location;

sealed public record CreateLocationDto(AddressDto AddressDto, string LocationName);

sealed public record AddressDto(string Country, string City, string Street);