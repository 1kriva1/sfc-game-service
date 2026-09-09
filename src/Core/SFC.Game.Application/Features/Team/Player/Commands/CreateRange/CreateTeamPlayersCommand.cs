using SFC.Game.Application.Common.Dto.Team.Player;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Team.Player.Commands.CreateRange;
public class CreateTeamPlayersCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreateTeamPlayers; }

    public IEnumerable<TeamPlayerDto> TeamPlayers { get; set; } = null!;
}