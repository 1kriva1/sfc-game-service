using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Get;

public class GetGameTeamPlayerQuery : Request<GetGameTeamPlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.GetGameTeamPlayer; }

    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }
}