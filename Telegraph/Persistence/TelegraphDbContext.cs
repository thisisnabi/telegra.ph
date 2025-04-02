
namespace Telegraph.Persistence;
  
public sealed class TelegraphDbContext(DbContextOptions<TelegraphDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public const string DatabaseDefaultSchema = "telegraph";
    public const string ConnectionStringName = "SvcDatabase";

    public DbSet<Letter> Letters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DatabaseDefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyMarker.ApplicationAssembly);
    }
}

