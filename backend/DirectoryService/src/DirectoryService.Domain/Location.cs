using DirectoryService.Domain.LocationVO;

namespace DirectoryService.Domain;

public sealed class Location
{
    private Location()
    {
    }

    private Location(LocationName name, Address address)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Address = address;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public LocationName Name { get; private set; } = LocationName.Create(string.Empty);
    public Address Address { get; private set; } = Address.Create("Country", "City", "Street");
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static Location Create(LocationName name, Address address)
    {
        return new Location(name, address);
    }
}