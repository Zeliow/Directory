namespace DirectoryService.Domain;

public sealed record ParentId
{
    public Guid? Value { get; }
    private ParentId(Guid? value)
    {
        Value = value;
    }
    public static ParentId Create(Guid? value)
    {
        if (value == Guid.Empty)
        {
            return new ParentId(Guid.Empty);
        }
        return new ParentId(value);
    }
}