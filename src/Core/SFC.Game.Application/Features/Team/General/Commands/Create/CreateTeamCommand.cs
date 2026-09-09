using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Team.General.Commands.Create;
public class CreateTeamCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreateTeam; }

    public TeamDto Team { get; set; } = null!;
}