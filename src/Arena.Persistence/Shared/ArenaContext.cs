namespace Arena.Persistence;

public class ArenaContext : DbContext
{
    public ArenaContext(DbContextOptions<ArenaContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArenaEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoundEntityConfiguration());
    }

    public DbSet<ArenaEntity> Arenas { get; set; } = null!;

}
