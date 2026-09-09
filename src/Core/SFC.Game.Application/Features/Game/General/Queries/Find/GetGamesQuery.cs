using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.General.Queries.Find;
public class GetGamesQuery : BasePaginationRequest<GetGamesViewModel, GetGamesFilterDto>
{
    public override RequestId RequestId { get => RequestId.GetGames; }
}