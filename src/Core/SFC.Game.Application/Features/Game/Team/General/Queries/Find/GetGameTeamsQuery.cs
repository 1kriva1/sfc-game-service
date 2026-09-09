using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Find;
public class GetGameTeamsQuery : BasePaginationRequest<GetGameTeamsViewModel, GetGameTeamsFilterDto>
{
    public override RequestId RequestId { get => RequestId.GetGameTeams; }

    public GetGameTeamsQuery SetGameId(long gameId)
    {
        Filter ??= new GetGameTeamsFilterDto();

        Filter.GameId = gameId;

        return this;
    }
}