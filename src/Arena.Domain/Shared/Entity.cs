namespace Arena.Domain;

public class Entity
{
    public Guid Guid { get; set; } = Guid.NewGuid();

    public float Change { get; set; }

    public string Role { get; set; } = null!;

    public float Health { get; set; }

    public float MaxHealth { get; set; }

    public virtual void Attack(Entity entity)
    {
        return;
    }

    public void Attacked()
    {
        if (Health / 2 < MaxHealth * 0.25f)
        {
            Change = Health;
            Health = 0;

            return;
        }

        Health /= 2;
        Change = Health;
    }

    public void RestoreHealth()
    {
        if (Health + 10 <= MaxHealth)
        {
            Health += 10;
        }
    }
}
