namespace DirectoryService.Domain.LocationVO;

public sealed record LocationName
{
    public string Value { get; }
    private LocationName(string value)
    {
        Value = value;
    }
    public static LocationName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Location name cannot be empty.", nameof(value));
        }
        return new LocationName(value);
    }
}