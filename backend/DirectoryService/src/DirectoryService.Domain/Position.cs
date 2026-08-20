namespace DirectoryService.Domain;

public sealed class Position
{
    private Position()
    {
    }

    public Position(string name, IEnumerable<Department> departments)
    {
        Id = Guid.CreateVersion7();
        Name = PositionName.Create(name);
        CreatedAt = DateTime.Now;
        _departments = departments.ToList();
    }

    private readonly List<Department> _departments = [];
    public Guid Id { get; private set; }
    public PositionName Name { get; private set; } = PositionName.Create(string.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public IReadOnlyList<Department> Departments => _departments;
}

public sealed record PositionName
{
    public string Value { get; }
    private PositionName(string value)
    {
        Value = value;
    }
    public static PositionName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Position name cannot be empty.", nameof(value));
        }
        return new PositionName(value);
    }
}