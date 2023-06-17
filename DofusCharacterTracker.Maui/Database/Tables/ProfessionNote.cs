using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DofusCharacterTracker.Maui.Database.Tables;

public sealed class ProfessionNote: IDbTable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required Guid CharacterId { get; set; }
    public Character? Character { get; set; }

    public required Profession Profession { get; set; }
    public required int Level { get; set; }
    public required string Notes { get; set; }

    public class Configuration : IEntityTypeConfiguration<ProfessionNote>
    {
        public void Configure(EntityTypeBuilder<ProfessionNote> builder)
        {
            builder.Property(x => x.Profession).HasConversion<string>().HasMaxLength(50);
        }
    }
}

public enum Profession
{
    Alchemist,
    Lumberjack,
    Miner,
    Artificer,
    Farmer,
    Fisher,
    Hunter,
    Jeweller,
    Shoemaker,
    Tailor,
    Carver,
    Smith,
    Handyman,
}
