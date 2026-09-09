namespace SFC.Game.Application.Common.Dto.Team.General.Filters;
public class TeamFilterDto
{
    public IEnumerable<int> Statuses { get; set; } = [];

    public TeamProfileFilterDto? Profile { get; set; }
}