namespace DirectoryService.Domain;

public sealed class DepartmentPosition
{
    private DepartmentPosition()
    {
    }

    public DepartmentPosition(Guid id, int departmentId, int positionId)
    {
        Id = id;
        DepartmentId = departmentId;
        PositionId = positionId;
    }

    public Guid Id { get; private set; }
    public int DepartmentId { get; private set; }
    public int PositionId { get; private set; }
}