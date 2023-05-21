using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DofusCharacterTracker.Database.Tables;

public sealed class Character: IDbTable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public required int Level { get; set; }

    public List<CharacterElement>? Elements { get; set; }
    public List<DungeonNote>? DungeonNotes { get; set; }
    public List<ProfessionNote>? ProfessionNotes { get; set; }

    public Class Class { get; set; }

    public required AlmanaxProgress AlmanaxProgress { get; set; }

    public bool UnlockedMoonIsland { get; set; }
    public bool UnlockedWabbitIsland { get; set; }
    public bool UnlockedPandala { get; set; }
    public bool UnlockedFrigost { get; set; }
    public bool UnlockedOtomaiIsland { get; set; }
    public bool UnlockedOtomaiBridgeOfDeath { get; set; }
    public bool UnlockedOhwymi { get; set; }

    public class Configuration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.Property(x => x.Class).HasConversion<string>().HasMaxLength(50);
            builder.Property(x => x.AlmanaxProgress).HasConversion<string>().HasMaxLength(50);
        }
    }
}

public enum Class
{
    Cra,
    Ecaflip,
    Eliotrope,
    Eniripsa,
    Enutrof,
    Feca,
    Foggernaut,
    Forgelance,
    Huppermage,
    Iop,
    Masqueraider,
    Osamodas,
    Ouginak,
    Pandawa,
    Rogue,
    Sacrier,
    Sadida,
    Sram,
    Xelor,
}

public enum AlmanaxProgress
{
    BarelyStarted,
    MaybeOneQuarter,
    AboutAHalf,
    AboutThreeQuarters,
    About80To90Percent,
    SoClose,
    Done
}

public static class AlmanaxProgressExtensions
{
    public static string GetDescription(this AlmanaxProgress progress) => progress switch
    {
        AlmanaxProgress.BarelyStarted => "~0%",
        AlmanaxProgress.MaybeOneQuarter => "~25%",
        AlmanaxProgress.AboutAHalf => "~50%",
        AlmanaxProgress.AboutThreeQuarters => "~75%",
        AlmanaxProgress.About80To90Percent => "~80-90%",
        AlmanaxProgress.SoClose => ">90%",
        AlmanaxProgress.Done => "100%!",
    };
}