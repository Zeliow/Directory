using DirectoryService.Domain.PositionVO;

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
        CreatedAt = DateTime.UtcNow;
        _departments = departments.ToList();
    }

    private readonly List<Department> _departments = [];
    public Guid Id { get; private set; }
    public PositionName Name { get; private set; } = PositionName.Create(string.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdateAt { get; private set; }
    public IReadOnlyList<Department> Departments => _departments;
}