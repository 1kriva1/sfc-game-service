namespace SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;
public class GetGamePlayersGamePlayerFilterDto
{
    public IEnumerable<int> Statuses { get; set; } = [];
}