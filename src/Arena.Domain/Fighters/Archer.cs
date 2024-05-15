namespace Arena.Domain;

public class Archer : Entity
{
    public Archer()
    {
        this.MaxHealth = 100;
        this.Health = 100;
        this.Role = nameof(Archer);
    }

    public override void Attack(Entity entity)
    {
        this.Change = 0;

        if(entity is MountedWarrier)
        {
            if(Utilities.Random.Next(1, 11) <= 4)
            {
                return;
            }

            entity.Attacked();
        }
        else
        {
            entity.Attacked();
        }
    }
}
