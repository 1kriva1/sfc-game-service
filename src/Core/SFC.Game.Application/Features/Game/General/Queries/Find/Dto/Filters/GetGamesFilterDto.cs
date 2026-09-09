namespace SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
public class GetGamesFilterDto
{
    public IEnumerable<int> Statuses { get; set; } = [];

    public GetGamesProfileFilterDto? Profile { get; set; }
}