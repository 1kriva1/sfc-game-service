using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find.Filters;

/// <summary>
/// Get games **profile filter** model.
/// </summary>
public class GetGamesProfileFilterModel : IMapTo<GetGamesProfileFilterDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public GetGamesGeneralProfileFilterModel? General { get; set; }

    /// <summary>
    /// Financial profile.
    /// </summary>
    public GetGamesFinancialProfileFilterModel? Financial { get; set; }

    /// <summary>
    /// Inventary profile.
    /// </summary>
    public GetGamesInventaryProfileFilterModel? Inventary { get; set; }
}