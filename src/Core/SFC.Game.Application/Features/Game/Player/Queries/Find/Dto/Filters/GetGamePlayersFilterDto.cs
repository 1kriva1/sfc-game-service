using SFC.Game.Application.Common.Dto.Player.General.Filters;

namespace SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;
public class GetGamePlayersFilterDto
{
    public long GameId { get; set; }

    public GetGamePlayersGamePlayerFilterDto? GamePlayer { get; set; }

    public PlayerFilterDto? Player { get; set; }
}