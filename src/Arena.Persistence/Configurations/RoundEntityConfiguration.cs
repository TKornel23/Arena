namespace Arena.Persistence;

internal class RoundEntityConfiguration : IEntityTypeConfiguration<Round>
{
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ToTable("ROUNDS", "ar");

        builder.HasKey(e => e.Guid);

        builder.Property(e => e.Guid)
            .HasColumnName("ID")
            .HasColumnType("nvarchar(36)");

        builder.Property(e => e.Id)
            .HasColumnName("ROUND");

        builder.Property(e => e.ArenaGuid)
            .HasColumnName("ARENA_GUID")
            .HasColumnType("nvarchar(36)");

        builder.HasOne(e => e.Arena)
            .WithMany(e => e.Rounds)
            .HasForeignKey(e => e.ArenaGuid);

        builder.OwnsOne(e => e.Attacker, owned => { owned.Property(e => e.Health).HasColumnName("ATTACKER_HEALTH"); });

        builder.OwnsOne(e => e.Attacker, owned => { owned.Property(e => e.MaxHealth).HasColumnName("ATTACKER_MAX_HEALTH"); });

        builder.OwnsOne(e => e.Attacker, owned => { owned.Property(e => e.Change).HasColumnName("ATTACKER_HEALTH_CHANGE"); });

        builder.OwnsOne(e => e.Attacker, owned => { owned.Property(e => e.Guid).HasColumnName("ATTACKER_GUID"); });

        builder.OwnsOne(e => e.Attacker, owned => { owned.Property(e => e.Role).HasColumnName("ATTACKER_ROLE"); });

        builder.OwnsOne(e => e.Defender, owned => { owned.Property(e => e.Health).HasColumnName("DEFENDER_HEALTH"); });

        builder.OwnsOne(e => e.Defender, owned => { owned.Property(e => e.MaxHealth).HasColumnName("DEFENDER_MAX_HEALTH"); });

        builder.OwnsOne(e => e.Defender, owned => { owned.Property(e => e.Change).HasColumnName("DEFENDER_HEALTH_CHANGE"); });

        builder.OwnsOne(e => e.Defender, owned => { owned.Property(e => e.Guid).HasColumnName("DEFENDER_GUID"); });

        builder.OwnsOne(e => e.Defender, owned => { owned.Property(e => e.Role).HasColumnName("DEFENDER_ROLE"); });
    }
}
