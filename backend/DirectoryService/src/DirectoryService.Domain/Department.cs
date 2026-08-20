using DirectoryService.Domain.DepartmentVO;

namespace DirectoryService.Domain;

public sealed class Department
{
    private Department()
    {
    }

    private Department(DepartmentName name,
                      Slug slug,
                      ParentId parentId,
                      DepartmentPath departmentPath,
                      IEnumerable<Location> locations,
                      IEnumerable<Position> positions)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        Slug = slug;
        ParentId = parentId;
        Path = departmentPath;
        CreatedAt = DateTime.Now;
        _locations = locations.ToList();
        _positions = positions.ToList();
    }

    private readonly List<Location> _locations = [];
    private readonly List<Position> _positions = [];
    public Guid Id { get; private set; }
    public DepartmentName Name { get; private set; } = DepartmentName.Create(string.Empty);
    public DepartmentPath? Path { get; private set; }
    public Slug Slug { get; private set; } = Slug.Create(string.Empty);
    public ParentId ParentId { get; private set; } = ParentId.Create(null);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Location> Locations => _locations;
    public IReadOnlyList<Position> Positions => _positions;

    public static Department Create(string name, string slug, string? parentPath, Guid? parentId, IEnumerable<Location> locations, IEnumerable<Position> positions)
    {
        var departmentName = DepartmentName.Create(name);
        var departmentSlug = Slug.Create(slug);
        var parentIdValue = ParentId.Create(parentId);

        var departmentPath = DepartmentPath.Create(departmentSlug.Value, parentPath);

        return new Department(departmentName, departmentSlug, parentIdValue, departmentPath, locations, positions);
    }
}