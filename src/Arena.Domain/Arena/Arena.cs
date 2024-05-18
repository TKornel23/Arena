using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arena.Domain;

public record Arena
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public int RoundCount { get; set; }
    public Status Status { get; set; } = Status.InProgress;

    [NotMapped]
    [JsonIgnore]
    public virtual List<Entity> Fighters { get; set; } = new();

    [NotMapped]
    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();
}
