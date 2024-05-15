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
    public IActionResult GetIdentifier(int count)
    {
        try
        {
            var id = this.FightingService.InitializeArena(count);

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
    public ActionResult<ArenaDTO> GetHistory(Guid guid)
    {
        try
        {
            var arena = this.FightingService.GetHistory(guid);

            return Ok(new ArenaEntity()
            {
                Rounds = arena.Rounds,
                Guid = arena.Guid,
                RoundCount = arena.Rounds.Count()
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
