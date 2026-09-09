namespace SFC.Game.Application.Interfaces.Team.Player;
public interface ITeamPlayerSeedService
{
    Task SendRequireTeamPlayersSeedAsync(CancellationToken cancellationToken = default);
}