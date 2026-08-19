namespace DirectoryService.Domain;

public sealed class DepartmentLocation
{
    private DepartmentLocation()
    {
    }

    public DepartmentLocation(Guid id, int departmentId, int locationId)
    {
        Id = id;
        DepartmentId = departmentId;
        LocationId = locationId;
    }

    public Guid Id { get; private set; }
    public int DepartmentId { get; private set; }
    public int LocationId { get; private set; }
}