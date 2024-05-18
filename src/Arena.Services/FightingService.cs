[assembly: InternalsVisibleTo("Arena.Services.Tests")]
namespace Arena.Services;

public class FightingService : IFightingService
{
    private IGenericRepository<ArenaEntity> ArenaRepository { get; set; }
    private IGenericRepository<Round> RoundRepository { get; set; }

    public FightingService(
        IGenericRepository<ArenaEntity> arenaRepository,
        IGenericRepository<Round> roundRepository)
    {
        this.ArenaRepository = arenaRepository;
        RoundRepository = roundRepository;

    }

    public async Task<ArenaEntity> GetHistory(Guid guid)
    {
        if(await this.ArenaRepository.Get(guid) is not ArenaEntity arena)
        {
            throw new InvalidGuidException($"Guid not found: {guid}");
        }

        arena.Rounds = (await this.RoundRepository.Get(x => x.ArenaGuid == guid)).ToList();

        return arena;
    }

    public async Task<Guid> InitializeArena(int count)
    {
        try
        {
            if (count == 0)
            {
                throw new ArgumentException("Arena count cannot be null.", nameof(count));
            }

            var arena = new ArenaEntity()
            {
                RoundCount = count
            };

            await this.ArenaRepository.Insert(arena);

            if(!await this.ArenaRepository.Save())
            {
                throw new InitializingFailedException("Arena initialization failed.");
            }

            HttpTriggerFightFunction(arena.Guid);

            return arena.Guid;
        }
        catch(ArgumentException)
        {
            throw;
        }
        catch
        {
            throw;
        }
    }

    internal static (Entity attacker, Entity defender) GetFighters(List<Entity> fighters)
    {
        if(fighters.Count(x => x.Health != 0) < 2)
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

        return (fighters.ElementAt(attackerPosition), fighters.ElementAt(defenderPosition));
    }

    private static string BuildUri(Guid guid)
    {
        var uriBuilder = new UriBuilder("http://localhost:7160/api/fight");

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["GUID"] = guid.ToString();

        uriBuilder.Query = query.ToString();

        return uriBuilder.ToString();
    }

    private static void HttpTriggerFightFunction(Guid guid)
    {
        var httpClient = new HttpClient();

        var uri = BuildUri(guid);

        //Fire and forget
        _ = Task.Run(async () => await httpClient.GetAsync(uri));
    }

    public static void InitializeFighters(ArenaEntity arena)
    {
        arena.Fighters = new List<Entity>();

        for (int i = 0; i < arena.RoundCount; i++)
        {
            arena.Fighters.Add(CreateEntity());
        }
    }

    public static (Entity winner, List<Round> rounds) FightPartition(List<Entity> entities, int roundIndex, Guid arenaGuid)
    {
        var rounds = new List<Round>();

        while (entities.Count != 1)
        {
            var (attacker, defender) = GetFighters(entities);

            attacker.Attack(defender);

            if (defender.Health == 0)
            {
                entities.Remove(defender);
            }

            if (attacker.Health == 0)
            {
                entities.Remove(attacker);
            }

            RestoreHealth(entities.Where(x => x.Guid != attacker.Guid && x.Guid != defender.Guid));

            rounds.Add(AddRound(attacker, defender, roundIndex, arenaGuid));

            roundIndex++;
        }

        return (entities[0], rounds);
    }

    private static Round AddRound(Entity attacker, Entity defender, int roundIndex, Guid arenaGuid)
    {
        return new()
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
            Id = roundIndex,
            ArenaGuid = arenaGuid
        };
    }

    public static void Fight(ArenaEntity arena)
    {
        var rangePartitioner = Partitioner.Create(0, arena.Fighters.Count, 100);

        var winners = new ConcurrentBag<Entity>();
        var rounds = new ConcurrentBag<Round>();

        Parallel.ForEach(rangePartitioner, (range, loopState) =>
        {
            var entites = arena.Fighters.Skip(range.Item1).Take(range.Item2 - range.Item1).ToList();

            var partition = FightPartition(entites, range.Item1, arena.Guid);

            winners.Add(partition.winner);
            partition.rounds.ForEach(x => rounds.Add(x));
        });

        var (_, lastRounds) = FightPartition(winners.ToList(), rounds.Count, arena.Guid);

        lastRounds.ForEach(x => rounds.Add(x));

        arena.Fighters = winners.ToList();
        arena.Rounds = rounds.ToList();
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
