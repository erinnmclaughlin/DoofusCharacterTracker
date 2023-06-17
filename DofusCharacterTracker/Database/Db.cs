using System.Reflection;
using DofusCharacterTracker.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker.Database;

public class Db: DbContext
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterElement> CharacterElements => Set<CharacterElement>();
    public DbSet<DungeonNote> DungeonNotes => Set<DungeonNote>();
    public DbSet<ProfessionNote> ProfessionNotes => Set<ProfessionNote>();

    public Db(DbContextOptions<Db> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
