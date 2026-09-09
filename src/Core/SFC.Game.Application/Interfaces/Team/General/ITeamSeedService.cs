namespace SFC.Game.Application.Interfaces.Team.General;
public interface ITeamSeedService
{
    Task SendRequireTeamsSeedAsync(CancellationToken cancellationToken = default);
}