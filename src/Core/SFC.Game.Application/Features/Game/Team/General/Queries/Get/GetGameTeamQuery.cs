using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Get;

public class GetGameTeamQuery : Request<GetGameTeamViewModel>
{
    public override RequestId RequestId { get => RequestId.GetGameTeam; }

    public long GameId { get; set; }

    public long TeamId { get; set; }
}