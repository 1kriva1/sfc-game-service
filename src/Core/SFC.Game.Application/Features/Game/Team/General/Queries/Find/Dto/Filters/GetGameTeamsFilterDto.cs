using SFC.Game.Application.Common.Dto.Team.General.Filters;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;
public class GetGameTeamsFilterDto
{
    public long GameId { get; set; }

    public GetGameTeamsGameTeamFilterDto? GameTeam { get; set; }

    public TeamFilterDto? Team { get; set; }
}