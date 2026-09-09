using SFC.Game.Application.Common.Dto.Team.Player;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Team.Player.Commands.Create;
public class CreateTeamPlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreateTeamPlayer; }

    public required TeamPlayerDto TeamPlayer { get; set; }
}