using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Update;
public class UpdateGameTeamPlayerDto : IMapTo<GameTeamPlayer>
{
    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }

    public int StatusId { get; set; }
}