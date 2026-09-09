using SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Find.Filters;

/// <summary>
/// Get game team players filter model.
/// </summary>
public class GetGameTeamPlayersFilterModel : IMapTo<GetGameTeamPlayersFilterDto>
{
    /// <summary>
    /// Game Team filter model.
    /// </summary>
    public GetGameTeamPlayersGameTeamPlayerFilterModel? GameTeamPlayer { get; set; }

    /// <summary>
    /// Player filter model.
    /// </summary>
    public PlayerFilterModel? Player { get; set; }
}