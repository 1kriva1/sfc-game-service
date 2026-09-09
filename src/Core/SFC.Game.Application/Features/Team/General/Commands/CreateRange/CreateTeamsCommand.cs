using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Team.General.Commands.CreateRange;
public class CreateTeamsCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreateTeams; }

    public IEnumerable<TeamDto> Teams { get; set; } = null!;
}