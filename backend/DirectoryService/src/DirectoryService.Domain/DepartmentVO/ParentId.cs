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
        return new ParentId(value);
    }
}