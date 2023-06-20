using System.Reflection;
using DoofusCharacterTracker.Database.Tables;
using Microsoft.EntityFrameworkCore;

namespace DoofusCharacterTracker.Database;

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
