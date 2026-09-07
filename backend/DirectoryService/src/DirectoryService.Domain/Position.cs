using DirectoryService.Domain.PositionVO;

namespace DirectoryService.Domain;

public sealed class Position
{
    private Position()
    {
    }

    public Position(string name, IEnumerable<DepartmentPosition> departments)
    {
        Id = Guid.CreateVersion7();
        Name = PositionName.Create(name);
        CreatedAt = DateTime.UtcNow;
        _departments = departments.ToList();
    }

    private readonly List<DepartmentPosition> _departments = [];
    public Guid Id { get; private set; }
    public PositionName Name { get; private set; } = PositionName.Create(string.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<DepartmentPosition> Departments => _departments;
}