using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.General.Queries.Get;

public class GetGameQuery : Request<GetGameViewModel>
{
    public override RequestId RequestId { get => RequestId.GetGame; }

    public long Id { get; set; }
}