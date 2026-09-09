using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Player.Queries.Get;

public class GetGamePlayerQuery : Request<GetGamePlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.GetGamePlayer; }

    public long GameId { get; set; }

    public long PlayerId { get; set; }
}