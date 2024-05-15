[assembly: InternalsVisibleTo("Arena.Services.Tests")]
namespace Arena.Services;

public class FightingService : IFightingService
{
    private IGenericRepository<ArenaEntity> ArenaRepository { get; set; }
    private IGenericRepository<Round> RoundsRepository { get; set; }

    public FightingService(
        IGenericRepository<ArenaEntity> arenaRepository,
        IGenericRepository<Round> roundsRepository)
    {
        this.ArenaRepository = arenaRepository;
        RoundsRepository = roundsRepository;

    }

    public ArenaEntity GetHistory(Guid guid)
    {
        if(this.ArenaRepository.Get(x => x.Guid == guid, includeProperties: "Rounds")
            .FirstOrDefault() is not ArenaEntity arena)
        {
            throw new InvalidGuidException($"Guid not found: {guid}");
        }

        arena.Rounds = arena.Rounds
            .OrderBy(x => x.Id)
            .ToList();

        return arena;
    }

    public Guid InitializeArena(int count)
    {
        if(count == 0)
        {
            throw new ArgumentException("Arena count cannot be null.", nameof(count));
        }

        var arena = new ArenaEntity()
        {
            RoundCount = count
        };        

        this.ArenaRepository.Insert(arena);

        this.ArenaRepository.Save();

        InitializeFighters(arena);

        Fight(arena);

        this.RoundsRepository.Insert(arena.Rounds);

        return this.ArenaRepository.Save()
            ? arena.Guid 
            : throw new InitializingFailedException("Arena initialization failed.");
    }

    internal static (Entity attacker, Entity defender) GetFighters(List<Entity> fighters)
    {
        if(fighters.Count < 2)
        {
            throw new ArgumentException("List count cannot be less than 2", nameof(fighters));
        }

        int attackerPosition;
        int defenderPosition;

        do
        {
            attackerPosition = Utilities.Random.Next(0, fighters.Count);
            defenderPosition = Utilities.Random.Next(0, fighters.Count);
        }
        while (attackerPosition == defenderPosition);

        return (fighters[attackerPosition], fighters[defenderPosition]);
    }

    public static void InitializeFighters(ArenaEntity arena)
    {
        arena.Fighters = new List<Entity>();

        for (int i = 0; i < arena.RoundCount; i++)
        {
            arena.Fighters.Add(CreateEntity());
        }
    }

    public static void Fight(ArenaEntity arena)
    {
        int index = 0;

        while (arena.Fighters!.Count != 1)
        {
            var (attacker, defender) = GetFighters(arena.Fighters);

            attacker.Attack(defender);

            if (defender.Health == 0)
            {
                arena.Fighters.Remove(defender);
            }

            if (attacker.Health == 0)
            {
                arena.Fighters.Remove(attacker);
            }

            RestoreHealth(arena.Fighters.Where(x => x.Guid != attacker.Guid && x.Guid != defender.Guid));

            arena.Rounds.Add(
                new()
                {
                    Attacker = new()
                    {
                        Change = attacker.Change,
                        MaxHealth = attacker.MaxHealth,
                        Guid = attacker.Guid,
                        Health = attacker.Health,
                        Role = attacker.Role,
                    },
                    Defender = new()
                    {
                        Change = defender.Change,
                        MaxHealth = defender.MaxHealth,
                        Guid = defender.Guid,
                        Health = defender.Health,
                        Role = defender.Role,
                    },
                    Id = index++,
                    ArenaGuid = arena.Guid,
                });
        }
    }

    internal static void RestoreHealth(IEnumerable<Entity> fighters)
    {
        foreach (var fighter in fighters)
        {
            fighter.RestoreHealth();
        }
    }

    internal static Entity CreateEntity()
    {
        var chance = Utilities.Random.Next(0, 101);

        if (chance <= 33)
        {
            return new MountedWarrier();
        }
        else if (chance <= 66)
        {
            return new Warrior();
        }
        else
        {
            return new Archer();
        }
    }
}
