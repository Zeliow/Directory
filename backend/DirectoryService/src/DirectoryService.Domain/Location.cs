using DirectoryService.Domain.LocationVO;

namespace DirectoryService.Domain;

public sealed class Location
{
    private Location()
    {
    }

    private Location(LocationName name, Address address, IEnumerable<DepartmentLocation> departments)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        _departments = departments.ToList();
    }

    private readonly List<DepartmentLocation> _departments = [];
    public Guid Id { get; private set; }
    public LocationName Name { get; private set; } = LocationName.Create(string.Empty);
    public Address Address { get; private set; } = Address.Create(string.Empty, string.Empty, string.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<DepartmentLocation> Departments => _departments;

    public static Location Create(string name, string country, string city, string street, IEnumerable<DepartmentLocation> departments)
    {
        var nameOfLocation = LocationName.Create(name);
        var address = Address.Create(country, city, street);

        return new Location(nameOfLocation, address, departments);
    }
}