using SFC.Game.Api.Infrastructure.Models.Game.Player.Find.Filters;
using SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.Team.Find.Filters;

/// <summary>
/// Get game players filter model.
/// </summary>
public class GetGamePlayersFilterModel : IMapTo<GetGamePlayersFilterDto>
{
    /// <summary>
    /// Game Team filter model.
    /// </summary>
    public GetGamePlayersGamePlayerFilterModel? GamePlayer { get; set; }

    /// <summary>
    /// Player filter model.
    /// </summary>
    public PlayerFilterModel? Player { get; set; }
}