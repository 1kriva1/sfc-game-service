using SFC.Game.Application.Common.Dto.Player.General.Filters;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;
public class GetGameTeamPlayersFilterDto
{
    public long GameId { get; set; }

    public long TeamId { get; set; }

    public GetGameTeamPlayersGameTeamPlayerFilterDto? GameTeamPlayer { get; set; }

    public PlayerFilterDto? Player { get; set; }
}