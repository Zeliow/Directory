namespace DirectoryService.Domain;

public sealed class Department
{
    private Department()
    {
    }

    public Department(string name,
                      string path,
                      string slug,
                      Guid? parentId,
                      DateTime createdAt,
                      DateTime updatedAt,
                      IEnumerable<Location> locations,
                      IEnumerable<Position> positions)
    {
        Id = Guid.CreateVersion7();
        Name = DepartmentName.Create(name);
        Path = Path.Create(path);
        Slug = Slug.Create(slug);
        ParentId = parentId.HasValue ? ParentId.Create(parentId.Value) : null;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _locations = locations.ToList();
        _positions = positions.ToList();
    }

    private readonly List<Location> _locations = [];
    private readonly List<Position> _positions = [];
    public Guid Id { get; private set; }
    public DepartmentName Name { get; private set; } = DepartmentName.Create(string.Empty);
    public Path Path { get; private set; } = Path.Create(string.Empty);
    public Slug Slug { get; private set; } = Slug.Create(string.Empty);
    public ParentId? ParentId { get; private set; } = ParentId.Create(Guid.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Location> Locations => _locations;
    public IReadOnlyList<Position> Positions => _positions;
}

public sealed record DepartmentName
{
    public string Value { get; }
    private DepartmentName(string value)
    {
        Value = value;
    }
    public static DepartmentName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Department name cannot be empty.", nameof(value));
        }
        return new DepartmentName(value);
    }
}

public sealed record Path
{
    public string Value { get; }
    private Path(string value)
    {
        Value = value;
    }
    public static Path Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Department path cannot be empty.", nameof(value));
        }
        return new Path(value);
    }
}

public sealed record Slug
{
    public string Value { get; }
    private Slug(string value)
    {
        Value = value;
    }
    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Department slug cannot be empty.", nameof(value));
        }
        return new Slug(value);
    }
}

public sealed record ParentId
{
    public Guid Value { get; }
    private ParentId(Guid value)
    {
        Value = value;
    }
    public static ParentId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return new ParentId(Guid.Empty);
        }
        return new ParentId(value);
    }
}