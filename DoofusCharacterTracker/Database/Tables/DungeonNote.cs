namespace DoofusCharacterTracker.Database.Tables;

public sealed class DungeonNote: IDbTable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required Guid CharacterId { get; set; }
    public Character? Character { get; set; }

    public required string DungeonName { get; set; }
    public required string Notes { get; set; }
}
