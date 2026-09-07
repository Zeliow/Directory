namespace DirectoryService.Domain;

public sealed class DepartmentLocation
{
    private DepartmentLocation()
    {
    }

    public DepartmentLocation(Department department, Guid locationId)
    {
        Id = Guid.CreateVersion7();
        Department = department;
        LocationId = locationId;
    }

    public Guid Id { get; private set; }
    public Guid LocationId { get; private set; }
    public Department Department { get; private set; } = null!;
}