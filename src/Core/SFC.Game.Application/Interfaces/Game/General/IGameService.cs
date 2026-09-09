namespace SFC.Game.Application.Interfaces.Game.General;
public interface IGameService
{
    Task NotifyGameCreatedAsync(GameEntity game, CancellationToken cancellationToken = default);

    Task NotifyGameUpdatedAsync(GameEntity game, CancellationToken cancellationToken = default);
}