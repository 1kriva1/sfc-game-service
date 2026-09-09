using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.Player.Queries.Find;
public class GetGamePlayersQuery : BasePaginationRequest<GetGamePlayersViewModel, GetGamePlayersFilterDto>
{
    public override RequestId RequestId { get => RequestId.GetGamePlayers; }

    public GetGamePlayersQuery SetGameId(long gameId)
    {
        Filter ??= new GetGamePlayersFilterDto();

        Filter.GameId = gameId;

        return this;
    }
}