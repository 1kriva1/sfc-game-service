using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Interfaces.Game.Player;
public interface IGamePlayerSeedService
{
    Task<IEnumerable<GamePlayer>> GetSeedGamePlayersAsync();

    Task SeedGamePlayersAsync(CancellationToken cancellationToken = default);
}