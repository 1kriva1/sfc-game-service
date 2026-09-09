using SFC.Game.Api.Infrastructure.Models.Team.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find.Filters;

/// <summary>
/// Get team players filter model.
/// </summary>
public class GetGameTeamsFilterModel : IMapTo<GetGameTeamsFilterDto>
{
    /// <summary>
    /// Game Team filter model.
    /// </summary>
    public GetGameTeamsGameTeamFilterModel? GameTeam { get; set; }

    /// <summary>
    /// Team filter model.
    /// </summary>
    public TeamFilterModel? Team { get; set; }
}