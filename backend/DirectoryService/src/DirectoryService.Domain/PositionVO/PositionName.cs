namespace DirectoryService.Domain.PositionVO;

public sealed record PositionName
{
    public string Value { get; }
    private PositionName(string value)
    {
        Value = value;
    }
    public static PositionName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Position name cannot be empty.", nameof(value));
        }
        return new PositionName(value);
    }
}