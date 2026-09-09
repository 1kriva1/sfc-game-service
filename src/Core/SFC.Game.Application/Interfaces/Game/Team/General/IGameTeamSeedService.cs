using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Interfaces.Game.Team.General;
public interface IGameTeamSeedService
{
    Task<IEnumerable<GameTeam>> GetSeedGameTeamsAsync();

    Task SeedGameTeamsAsync(CancellationToken cancellationToken = default);
}