using DirectoryService.Domain.DepartmentVO;

namespace DirectoryService.Domain;

public sealed class Department
{
    //EF Core
    private Department()
    {
    }

    private Department(DepartmentName name,
                      Slug slug,
                      ParentId? parentId,
                      DepartmentPath? departmentPath)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Slug = slug;
        ParentId = parentId;
        Path = departmentPath;
        CreatedAt = DateTime.UtcNow;
    }

    private readonly List<DepartmentLocation> _locations = [];
    private readonly List<DepartmentPosition> _positions = [];
    public Guid Id { get; private set; }
    public DepartmentName Name { get; private set; } = null!;
    public DepartmentPath? Path { get; private set; }
    public Slug Slug { get; private set; } = null!;
    public ParentId? ParentId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<DepartmentLocation> Locations => _locations;
    public IReadOnlyList<DepartmentPosition> Positions => _positions;

    public static Department Create(
        DepartmentName name,
        Slug slug,
        DepartmentPath? parentPath,
        ParentId? parentId,
        IEnumerable<Guid> locations)
    {
        var departmentPath = DepartmentPath.Create(slug.Value, parentPath?.Value);
        var department = new Department(name, slug, parentId, departmentPath);

        foreach (var id in locations)
        {
            department.AddLocation(id);
        }

        return department;
    }

    public bool AddLocation(Guid locationId)
    {
        if (_locations.Any(l => l.LocationId == locationId)) return false;
        _locations.Add(new DepartmentLocation(this, locationId));
        return true;
    }

    public bool RemoveLocation(Guid locationId)
    {
        var location = _locations.FirstOrDefault(l => l.LocationId == locationId);
        if (location == null) return false;
        _locations.Remove(location);
        return true;
    }

    public void UpdateDepartmentName(DepartmentName newName)
    {
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }
}