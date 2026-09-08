namespace DirectoryService.Domain;

public sealed class DepartmentPosition
{
    private DepartmentPosition()
    {
    }

    public DepartmentPosition(Department department, Guid positionId)
    {
        Id = Guid.CreateVersion7();
        Department = department;
        PositionId = positionId;
    }

    public Guid Id { get; private set; }
    public Guid PositionId { get; private set; }
    public Department Department { get; private set; } = null!;
}