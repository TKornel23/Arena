namespace Arena.Persistence;

internal class ArenaEntityConfiguration : IEntityTypeConfiguration<ArenaEntity>
{
    public void Configure(EntityTypeBuilder<ArenaEntity> builder)
    {
        builder.ToTable("Arena", "ar");

        builder.HasKey(e => e.Guid);

        builder.Property(e => e.Status)
            .HasColumnName("STATUS")
            .HasConversion(
                v => v.ToString(),
                v => ParseEnum(v));

        builder.Property(e => e.Guid)
            .HasColumnName("ID")
            .HasColumnType("nvarchar(36)");

        builder.Property(e => e.RoundCount)
            .HasColumnName("ROUND_COUNT");
    }

    private static Status ParseEnum(string input)
    {
        if (Enum.TryParse<Status>(input, true, out var res))
        {
            return res;
        }

        return default;
    }
}

