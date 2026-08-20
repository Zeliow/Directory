using System.Text.RegularExpressions;

namespace DirectoryService.Domain.DepartmentVO;

public sealed record Slug
{
    public string Value { get; }
    private Slug(string value)
    {
        Value = value;
    }
    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, "^[a-z0-9-]+$"))
        {
            throw new ArgumentException("Department slug cannot be empty or use digits", nameof(value));
        }
        return new Slug(value);
    }
}