using SFC.Game.Application.Common.Dto.Player.General;

namespace SFC.Game.Application.Interfaces.Player;
public interface IPlayerService
{
    Task<PlayerDto?> GetPlayerAsync(long id, CancellationToken cancellationToken = default);
}