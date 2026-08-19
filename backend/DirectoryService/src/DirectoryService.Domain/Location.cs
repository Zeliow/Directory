namespace DirectoryService.Domain;

public sealed class Location
{
    private Location()
    {
    }

    public Location(Guid id, string name, string address, DateTime createdAt, DateTime updatedAt, IEnumerable<Department> departments)
    {
        Id = id;
        Name = name;
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _departments = departments.ToList();
    }

    private readonly List<Department> _departments = [];
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Department> Departments => _departments;
}