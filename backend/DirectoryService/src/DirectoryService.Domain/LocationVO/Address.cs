namespace DirectoryService.Domain.LocationVO;

public sealed record Address
{
    public string Value { get; }

    private Address(string country, string city, string street)
    {
        Value = $"{country}, {city}, {street}";
    }
    public static Address Create(string country, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(country))
        {
            throw new ArgumentException("Address components cannot be empty.", nameof(street));
        }
        return new Address(country, city, street);
    }
}