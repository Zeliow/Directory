namespace DirectoryService.Contracts.Location;

sealed public record UpdateLocationDto(AddressDto AddressDto, string LocationName);