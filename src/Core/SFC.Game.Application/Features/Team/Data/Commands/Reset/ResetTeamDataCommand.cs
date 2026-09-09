using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Team.Data.Common.Dto;

namespace SFC.Game.Application.Features.Team.Data.Commands.Reset;
public class ResetTeamDataCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.ResetTeamData; }

    public IEnumerable<TeamPlayerStatusDto> TeamPlayerStatuses { get; init; } = [];
}