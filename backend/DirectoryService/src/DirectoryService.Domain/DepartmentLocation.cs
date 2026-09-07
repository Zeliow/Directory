namespace DirectoryService.Domain;

public sealed class DepartmentLocation
{
    private DepartmentLocation()
    {
    }

    public DepartmentLocation(Guid id, Guid departmentId, Guid locationId)
    {
        Id = id;
        DepartmentId = departmentId;
        LocationId = locationId;
    }

    public Guid Id { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid LocationId { get; private set; }

    public Department Department { get; private set; } = null!;
    public Location Location { get; private set; } = null!;
}