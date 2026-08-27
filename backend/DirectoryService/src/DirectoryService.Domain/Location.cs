using DirectoryService.Domain.LocationVO;

namespace DirectoryService.Domain;

public sealed class Location
{
    private Location()
    {
    }

    public Location(string name, string address, IEnumerable<Department> departments)
    {
        Id = Guid.CreateVersion7();
        Name = LocationName.Create(name);
        Address = Address.Create(address);
        CreatedAt = DateTime.UtcNow;
        _departments = departments.ToList();
    }

    private readonly List<Department> _departments = [];
    public Guid Id { get; private set; }
    public LocationName Name { get; private set; } = LocationName.Create(string.Empty);
    public Address Address { get; private set; } = Address.Create(string.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Department> Departments => _departments;
}