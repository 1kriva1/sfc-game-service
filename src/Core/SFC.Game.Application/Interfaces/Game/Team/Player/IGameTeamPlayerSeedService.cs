using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Interfaces.Game.Team.Player;
public interface IGameTeamPlayerSeedService
{
    Task<IEnumerable<GameTeamPlayer>> GetSeedGameTeamPlayersAsync();

    Task SeedGameTeamPlayersAsync(CancellationToken cancellationToken = default);
}