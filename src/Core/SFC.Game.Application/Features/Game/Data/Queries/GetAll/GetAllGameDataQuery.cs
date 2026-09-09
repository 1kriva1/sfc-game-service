using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Data.Queries.GetAll;

public class GetAllGameDataQuery : Request<GetAllGameDataViewModel>
{
    public override RequestId RequestId { get => RequestId.GetAllGameData; }
}