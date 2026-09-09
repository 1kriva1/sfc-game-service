using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find.Filters;

/// <summary>
/// Get games **financial profile filter** model.
/// </summary>
public class GetGamesFinancialProfileFilterModel : IMapTo<GetGamesFinancialProfileFilterDto>
{
    /// <summary>
    /// Describe if game is free to play.
    /// </summary>
    public bool? FreeGame { get; set; }

    /// <summary>
    /// How many need to pay for game.
    /// </summary>
    public RangeLimitDto<decimal?>? PayAmount { get; set; }
}