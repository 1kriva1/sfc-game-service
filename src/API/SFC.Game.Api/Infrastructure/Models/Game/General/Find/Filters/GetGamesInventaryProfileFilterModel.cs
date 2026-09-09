using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find.Filters;

/// <summary>
/// Get games **inventary profile filter** model.
/// </summary>
public class GetGamesInventaryProfileFilterModel : IMapTo<GetGamesInventaryProfileFilterDto>
{
    /// <summary>
    /// Game's **shirts required**.
    /// </summary>
    public bool? ShirtsRequired { get; set; }

    /// <summary>
    /// How many shirts are required for game.
    /// </summary>
    public RangeLimitDto<int?>? ShirtsCount { get; set; }
}