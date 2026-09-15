namespace DirectoryService.Contracts.Location;

sealed public record CreateLocationDto(AddressDto AddressDto, string LocationName);