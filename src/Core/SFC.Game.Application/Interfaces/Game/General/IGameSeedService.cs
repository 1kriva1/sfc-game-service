namespace SFC.Game.Application.Interfaces.Game.General;
public interface IGameSeedService
{
    Task<IEnumerable<GameEntity>> GetSeedGamesAsync();

    Task SeedGamesAsync(CancellationToken cancellationToken = default);
}