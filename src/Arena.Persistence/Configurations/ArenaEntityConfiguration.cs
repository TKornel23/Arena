namespace Arena.Persistence;

internal class ArenaEntityConfiguration : IEntityTypeConfiguration<ArenaEntity>
{
    public void Configure(EntityTypeBuilder<ArenaEntity> builder)
    {
        builder.ToTable("Arena", "ar");

        builder.HasKey(e => e.Guid);

        //builder.HasMany(e => e.History)
        //    .WithOne(e => e.Arena)
        //    .HasForeignKey(e => e.ArenaId);

        builder.Property(e => e.Guid)
            .HasColumnName("ID")
            .HasColumnType("nvarchar(36)");

        builder.Property(e => e.RoundCount)
            .HasColumnName("ROUND_COUNT");
    }
}

