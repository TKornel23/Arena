namespace Arena.Domain.Tests;

public class EntityTests
{
    [Fact]
    public void Attacked_Should_Lower_Health()
    {
        var warrior = new Warrior();

        warrior.Attacked();

        warrior.Health.Should().Be(60);

        warrior.Attacked();

        warrior.Health.Should().Be(30);

        warrior.Attacked();

        warrior.Health.Should().Be(0);
    }

    [Fact]
    public void Attacked_Should_Set_Changed_As_Expected()
    {
        var warrior = new Warrior();

        warrior.Attacked();

        warrior.Change.Should().Be(60);

        warrior.Attacked();

        warrior.Change.Should().Be(30);

        warrior.Attacked();

        warrior.Change.Should().Be(30);
    }

    [Fact]
    public void RestoreHealth_Should_IncreaseHealth()
    {
        var warrior = new Warrior
        {
            Health = 110
        };

        warrior.RestoreHealth();

        warrior.Health.Should().Be(120);
    }

    [Fact]
    public void RestoreHealth_Should_Not_IncreaseHealth()
    {
        var warrior = new Warrior();

        warrior.RestoreHealth();

        warrior.Health.Should().Be(120);
    }

    [Fact]
    public void Warrior_Attack_MountedWarrior_Should_Do_Nothing()
    {
        var warrior = new Warrior();
        var mounted = new MountedWarrier();

        warrior.Attack(mounted);

        mounted.Health.Should().Be(150);
    }

    [Fact]
    public void Warrior_Attack_Warrior_Should_Decrease_Defender_Health()
    {
        var warrior = new Warrior();
        var defender = new Warrior();

        warrior.Attack(defender);

        defender.Health.Should().Be(60);
    }

    [Fact]
    public void Warrior_Attack_Archer_Should_Decrease_Defender_Health()
    {
        var warrior = new Warrior();
        var defender = new Archer();

        warrior.Attack(defender);

        defender.Health.Should().Be(50);
    }

    [Fact]
    public void Archer_Attack_Archer_Should_Decrease_Defender_Health()
    {
        var archer = new Archer();
        var defender = new Archer();

        archer.Attack(defender);

        defender.Health.Should().Be(50);
    }

    [Fact]
    public void Archer_Attack_Warrior_Should_Decrease_Defender_Health()
    {
        var archer = new Archer();
        var defender = new Warrior();

        archer.Attack(defender);

        defender.Health.Should().Be(60);
    }

    [Fact]
    public void MountedWarrior_Attack_Warrior_Should_Decrease_MountedWarrior_Health()
    {
        var mountedWarrior = new MountedWarrier();
        var defender = new Warrior();

        mountedWarrior.Attack(defender);

        mountedWarrior.Health.Should().Be(75);
        defender.Health.Should().Be(120);
    }

    [Fact]
    public void MountedWarrior_Attack_Archer_Should_Decrease_Defender_Health()
    {
        var mountedWarrior = new MountedWarrier();
        var defender = new Archer();

        mountedWarrior.Attack(defender);

        defender.Health.Should().Be(50);
    }

    [Fact]
    public void MountedWarrior_Attack_MountedWarrior_Should_Decrease_Defender_Health()
    {
        var mountedWarrior = new MountedWarrier();
        var defender = new MountedWarrier();

        mountedWarrior.Attack(defender);

        defender.Health.Should().Be(75);
    }
}