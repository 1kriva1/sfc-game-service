using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Find;
public class GetGameTeamPlayersQuery : BasePaginationRequest<GetGameTeamPlayersViewModel, GetGameTeamPlayersFilterDto>
{
    public override RequestId RequestId { get => RequestId.GetGameTeamPlayers; }

    public GetGameTeamPlayersQuery SetGameId(long gameId)
    {
        Filter ??= new GetGameTeamPlayersFilterDto();

        Filter.GameId = gameId;

        return this;
    }

    public GetGameTeamPlayersQuery SetTeamId(long teamId)
    {
        Filter ??= new GetGameTeamPlayersFilterDto();

        Filter.TeamId = teamId;

        return this;
    }
}