namespace Arena.Domain;

public class Warrior : Entity
{
    public Warrior()
    {
        this.MaxHealth = 120;
        this.Health = 120;
        this.Role = nameof(Warrior);
    }

    public override void Attack(Entity entity)
    {
        this.Change = 0;

        if (entity is MountedWarrier)
        {
            return;
        }

        entity.Attacked();
    }
}
