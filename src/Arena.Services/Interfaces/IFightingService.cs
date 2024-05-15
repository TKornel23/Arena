namespace Arena.Services;

public interface IFightingService
{
    Guid InitializeArena(int count);
    ArenaEntity GetHistory(Guid guid);
}