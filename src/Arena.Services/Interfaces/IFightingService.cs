namespace Arena.Services;

public interface IFightingService
{
    Task<Guid> InitializeArena(int count);
    Task<ArenaEntity> GetHistory(Guid guid);
}