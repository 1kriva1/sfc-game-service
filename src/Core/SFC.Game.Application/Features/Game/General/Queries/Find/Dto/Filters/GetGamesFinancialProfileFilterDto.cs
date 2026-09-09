using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
public class GetGamesFinancialProfileFilterDto
{
    public bool? FreeGame { get; set; }

    public RangeLimitDto<decimal?>? PayAmount { get; set; }
}