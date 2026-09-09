using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Team.General.Commands.Update;
public class UpdateTeamCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdateTeam; }

    public TeamDto Team { get; set; } = null!;
}