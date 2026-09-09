namespace SFC.Game.Application.Interfaces.Player;
public interface IPlayerSeedService
{
    Task SendRequirePlayersSeedAsync(CancellationToken cancellationToken = default);
}