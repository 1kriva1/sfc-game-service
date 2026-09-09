using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Gets;

public class GetsGameTeamQuery : Request<GetsGameTeamViewModel>
{
    public override RequestId RequestId { get => RequestId.GetsGameTeam; }

    public long GameId { get; set; }
}