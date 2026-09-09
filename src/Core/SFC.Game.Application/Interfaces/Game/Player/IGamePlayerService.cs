using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Interfaces.Game.Player;
public interface IGamePlayerService
{
    Task NotifyGamePlayerCreatedAsync(GamePlayer gamePlayer, CancellationToken cancellationToken = default);

    Task NotifyGamePlayerUpdatedAsync(GamePlayer gamePlayer, CancellationToken cancellationToken = default);
}