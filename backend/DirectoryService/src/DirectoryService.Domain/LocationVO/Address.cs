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
    public static Address CreateVO(string value)
    {
        var components = value.Split(',');
        if (components.Length != 3)
        {
            throw new ArgumentException("Invalid address format.", nameof(value));
        }

        var country = components[0].Trim();
        var city = components[1].Trim();
        var street = components[2].Trim();

        return new Address(country, city, street);
    }
}