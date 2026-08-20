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
            throw new ArgumentException("Slug должен содержать только строчные буквы, цифры и дефисы.", nameof(value));
        }
        return new Slug(value);
    }
}