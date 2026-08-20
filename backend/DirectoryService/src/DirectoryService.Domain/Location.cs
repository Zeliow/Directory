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
        CreatedAt = DateTime.Now;
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

public sealed record LocationName
{
    public string Value { get; }
    private LocationName(string value)
    {
        Value = value;
    }
    public static LocationName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Location name cannot be empty.", nameof(value));
        }
        return new LocationName(value);
    }
}

public sealed record Address
{
    public string Value { get; }
    private Address(string value)
    {
        Value = value;
    }
    public static Address Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Address cannot be empty.", nameof(value));
        }
        return new Address(value);
    }
}