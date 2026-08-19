namespace DirectoryService.Domain;

public sealed class Department
{
    private Department()
    {
    }

    public Department(Guid id,
                      string name,
                      string path,
                      string slug,
                      int parentId,
                      DateTime createdAt,
                      DateTime updatedAt,
                      IEnumerable<Location> locations,
                      IEnumerable<Position> positions)
    {
        Id = id;
        Name = name;
        Path = path;
        Slug = slug;
        ParentId = parentId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _locations = locations.ToList();
        _positions = positions.ToList();
    }

    private readonly List<Location> _locations = [];
    private readonly List<Position> _positions = [];
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public int ParentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Location> Locations => _locations;
    public IReadOnlyList<Position> Positions => _positions;
}