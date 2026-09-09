using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Commands.Update;
public class UpdateGamePlayerDto : IMapTo<GamePlayer>
{
    public long GameId { get; set; }

    public long PlayerId { get; set; }

    public int? StatusId { get; set; }
}