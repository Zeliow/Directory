using DirectoryService.Domain.PositionVO;

namespace DirectoryService.Domain;

public sealed class Position
{
    private Position()
    {
    }

    public Position(PositionName name)
    {
        Id = Guid.CreateVersion7();
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public PositionName Name { get; private set; } = PositionName.Create(string.Empty);
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static Position Create(PositionName positionName)
    {
        return new Position(positionName);
    }
}