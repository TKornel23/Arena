namespace Arena.Domain;

public record ArenaDTO
{
    public Guid Id { get; set; }

    public List<Entity> Fighters { get; set; } = new();

    public ICollection<Round> Rounds { get; set; } = null!;

    public int RoundCount { get; set; }
}
