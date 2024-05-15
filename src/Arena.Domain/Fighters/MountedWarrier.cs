namespace Arena.Domain;

public class MountedWarrier : Entity
{
    public MountedWarrier()
    {
        this.Health = 150;
        this.MaxHealth = 150;
        this.Role = nameof(MountedWarrier);
    }

    public override void Attack(Entity entity)
    {
        if(entity is Warrior)
        {
            Attacked();
        }
        else
        {
            this.Change = 0;

            entity.Attacked();
        }
    }
}
