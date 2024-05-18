namespace Arena.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArenaController : ControllerBase
{
    private IFightingService FightingService { get; set; }

    public ArenaController(IFightingService fighterService)
    {
        this.FightingService = fighterService;
    }

    [HttpGet("{count:int}")]
    public async Task<IActionResult> GetIdentifier(int count)
    {
        try
        {
            var id = await this.FightingService.InitializeArena(count);

            return Ok(id);
        }
        catch(InitializingFailedException ifex)
        {
            return BadRequest(ifex.Message);
        }
        catch(ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch
        {
            throw;
        }
    }

    [HttpGet("history/{guid}")]
    public async Task<ActionResult<ArenaDTO>> GetHistory(Guid guid)
    {
        try
        {
            var arena = await this.FightingService.GetHistory(guid);

            if(arena.Status == Status.Failed)
            {
                throw new ArenaFailedException("Fighting failed");
            }

            if(arena.Status == Status.InProgress)
            {
                return Ok("Fighting is still in progress");
            }

            return Ok(new ArenaEntity()
            {
                Rounds = arena.Rounds,
                Guid = arena.Guid,
                RoundCount = arena.Rounds.Count
            });
        }
        catch(ArenaFailedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest();
        }
    }
}
