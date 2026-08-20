namespace DirectoryService.Domain.DepartmentVO;

public sealed record DepartmentName
{
    public string Value { get; }
    private DepartmentName(string value)
    {
        Value = value;
    }
    public static DepartmentName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Department name cannot be empty.", nameof(value));
        }
        return new DepartmentName(value);
    }
}
