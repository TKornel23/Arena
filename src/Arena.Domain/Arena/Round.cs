using System.Text.Json.Serialization;

namespace Arena.Domain;

public record Round
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public int Id { get; set; }

    public Guid ArenaGuid { get; set; }

    [JsonIgnore]
    public virtual Arena Arena { get; set; } = null!;

    public Entity Attacker { get; set; } = null!;

    public Entity Defender { get; set; } = null!;
}