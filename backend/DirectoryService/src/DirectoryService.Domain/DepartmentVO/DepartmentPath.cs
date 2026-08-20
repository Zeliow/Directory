using System.Text.RegularExpressions;

namespace DirectoryService.Domain.DepartmentVO;

public sealed record DepartmentPath
{
    private const string SEPARATOR = "/";
    public string Value { get; }

    private DepartmentPath(string value)
    {
        Value = value;
    }
    public static DepartmentPath Create(string slug, string? parentPath = null)
    {
        if (parentPath == null)
        {
            return new DepartmentPath(SEPARATOR + slug);
        }
        if (!Regex.IsMatch(parentPath, "^/([a-z0-9-]+/)*[a-z0-9-]+$"))
        {
            throw new ArgumentException("Invalid department path format", nameof(parentPath));
        }

        return new DepartmentPath(parentPath + SEPARATOR + slug);
    }
}