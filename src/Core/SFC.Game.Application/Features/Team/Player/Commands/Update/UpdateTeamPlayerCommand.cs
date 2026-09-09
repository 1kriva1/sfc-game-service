using SFC.Game.Application.Common.Dto.Team.Player;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Team.Player.Commands.Update;
public class UpdateTeamPlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdateTeamPlayer; }

    public required TeamPlayerDto TeamPlayer { get; set; }
}