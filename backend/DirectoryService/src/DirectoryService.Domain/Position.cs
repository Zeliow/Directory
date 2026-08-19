namespace DirectoryService.Domain;

public sealed class Position
{
    private Position()
    {
    }

    public Position(Guid id, string name, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdateAt = updatedAt;
    }

    public Position(Guid id, string name, DateTime createdAt, DateTime updateAt, IEnumerable<Department> departments)
    {
        _departments = departments.ToList();
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdateAt = updateAt;
    }

    private readonly List<Department> _departments = [];
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public IReadOnlyList<Department> Departments => _departments;
}