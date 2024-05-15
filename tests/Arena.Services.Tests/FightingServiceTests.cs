using System.Linq.Expressions;

namespace Arena.Services.Tests;

public class FightingServiceTests
{
    [Fact]
    public void GetFighters_With_Attacker_And_Defender_Should_Not_Be_Null()
    {
        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var fightingervice = new FightingService(repositoryMock.Object, Mock.Of<IGenericRepository<Round>>());

        var (attacker, defender) = FightingService.GetFighters(new()
        {
            new Warrior() { MaxHealth = 100, Role = nameof(Warrior) },
            new Archer() { MaxHealth = 120, Role = nameof(Archer) }
        });

        attacker.Should().NotBeNull();
        defender.Should().NotBeNull();
    }

    [Fact]
    public void GetFighters_With_One_Item_In_List_Should_Throw_Exception()
    {
        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var fightingervice = new FightingService(repositoryMock.Object, Mock.Of<IGenericRepository<Round>>());

        var action = () => FightingService.GetFighters(new()
        {
            new Warrior() { MaxHealth = 100, Role = nameof(Warrior) }
        });

        action.Should()
            .Throw<ArgumentException>()
            .WithMessage("List count cannot be less than 2 (Parameter 'fighters')");
    }

    [Fact]
    public void GetHistory_With_Not_Found_Guid_Should_Throw_InvalidGuidException()
    {
        const string guid = "1A3B944E-3632-467B-A53A-206305310BAE";

        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var fightingervice = new FightingService(repositoryMock.Object, Mock.Of<IGenericRepository<Round>>());

        var action = () => fightingervice.GetHistory(Guid.Parse(guid));

        action.Should()
            .Throw<InvalidGuidException>()
            .WithMessage($"Guid not found: {guid}");
    }

    [Fact]
    public void GetHistory_With_Valid_Guid_Should_Return_Arena()
    {
        const string stringGuid = "1A3B944E-3632-467B-A53A-206305310BAE";

        var guid = Guid.Parse(stringGuid);

        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var arena = new ArenaEntity()
        {
            Guid = guid,
            Fighters = new List<Entity>(),
            RoundCount = 2,
        };

        repositoryMock
            .Setup(x => x.Get(x => x.Guid == guid, null, "Rounds"))
            .Returns(new List<ArenaEntity>() { arena });

        var fightingervice = new FightingService(repositoryMock.Object, Mock.Of<IGenericRepository<Round>>());

        var result = fightingervice.GetHistory(arena.Guid);

        result.Guid.Should().Be(arena.Guid);
    }

    [Fact]
    public void InitializeArena_With_Zero_Count_Should_Throw_ArgumentExceptin()
    {
        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var fightingervice = new FightingService(repositoryMock.Object, Mock.Of<IGenericRepository<Round>>());

        var action = () => fightingervice.InitializeArena(0);

        action.Should().
            Throw<ArgumentException>()
            .WithMessage("Arena count cannot be null. (Parameter 'count')");
    }

    [Fact]
    public void InitializeArena_Error_Saving_Arena_Should_Throw_InitializingFailedException()
    {
        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var fightingervice = new FightingService(repositoryMock.Object, Mock.Of<IGenericRepository<Round>>());

        var action = () => fightingervice.InitializeArena(1);

        action.Should().
            Throw<InitializingFailedException>()
            .WithMessage("Arena initialization failed.");
    }

    [Fact]
    public void InitializeArena_Succesfull_Saving_Arena_Should_Return_Guid()
    {
        var repositoryMock = new Mock<IGenericRepository<ArenaEntity>>();

        var roundRepositoryMock = new Mock<IGenericRepository<Round>>();

        roundRepositoryMock
            .Setup(x => x.Save())
            .Returns(true);

        repositoryMock
            .Setup(x => x.Save())
            .Returns(true);

        var fightingervice = new FightingService(repositoryMock.Object, roundRepositoryMock.Object);

        fightingervice.InitializeArena(1).Should().NotBeEmpty();
    }

    [Fact]
    public void RestoreHealth_Should_Increase_Health()
    {
        var fighters = new List<Entity>()
        {
            new Warrior() { MaxHealth = 100, Health = 90 },
            new Archer() { MaxHealth = 120, Health = 110 },
        };

        FightingService.RestoreHealth(fighters);

        fighters[0].Health.Should().Be(100);
        fighters[1].Health.Should().Be(120);
    }

    [Fact]
    public void RestoreHealth_Should_Not_Increase_Health()
    {
        var fighters = new List<Entity>()
        {
            new Warrior() { MaxHealth = 100, Health = 100 },
            new Archer() { MaxHealth = 150, Health = 150 },
        };

        FightingService.RestoreHealth(fighters);

        fighters[0].Health.Should().Be(100);
        fighters[1].Health.Should().Be(150);
    }

    [Fact]
    public void CreateEntity_Should_Create_Entity_With_Parent_Type_Entity()
    {
        FightingService.CreateEntity()
            .Should().NotBeNull()
            .And.BeAssignableTo<Entity>();
    }

    [Fact]
    public void Fight_Should_Return_Arena_With_Properties()
    {
        var warriors = new Fixture()
                .Build<Warrior>()
                .With(x => x.Role, nameof(Warrior))
                .With(x => x.MaxHealth, 100)
                .With(x => x.Health, 100)
                .With(x => x.Change, 0)
                .CreateMany(2)
                .Cast<Entity>()
                .ToList();

        var arena = new ArenaEntity()
        {
            RoundCount = 2,
            Fighters = warriors
        };

        FightingService.Fight(arena);

        arena.Fighters!.Count.Should().Be(1);
        arena.Rounds.Count.Should().BeGreaterThan(0);
        arena.Guid.Should().NotBeEmpty();
    }
}