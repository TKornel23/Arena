namespace Arena.AzureApi;

public class FightHttpTrigger
{
    private readonly IGenericRepository<Round> RoundRepository;
    private readonly IGenericRepository<Domain.Arena> ArenaRepository;

    public FightHttpTrigger(
        IGenericRepository<Round> roundRepository,
        IGenericRepository<Domain.Arena> arenaRepository)
    {
        this.RoundRepository = roundRepository;
        this.ArenaRepository = arenaRepository;
    }

    [FunctionName("fight")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Fight HTTP Trigger process started.");

        try
        {
            var guid = Guid.Parse(req.Query["guid"]);

            log.BeginScope(guid);

            if (await this.ArenaRepository.Get(guid) is not Domain.Arena arena)
            {
                return new BadRequestObjectResult($"Arena is missing");
            }

            log.LogInformation($"Arena is found.");

            FightingService.InitializeFighters(arena);

            log.LogInformation($"Initialize fighters is successful.");

            FightingService.Fight(arena);

            log.LogInformation($"Fight is successfull");

            await this.RoundRepository.Insert(arena.Rounds);

            await this.RoundRepository.Save();

            arena.Status = Status.Finished;

            this.ArenaRepository.Update(arena);

            await this.ArenaRepository.Save();

            log.LogInformation("Rounds successfully saved to the database");

            return new OkResult();
        }
        catch(Exception ex)
        {
            log.LogError(ex.Message, ex);

            return new BadRequestObjectResult(ex);
        }
    }
}
