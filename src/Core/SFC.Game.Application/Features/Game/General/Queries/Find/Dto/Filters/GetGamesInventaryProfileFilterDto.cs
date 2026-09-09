using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
public class GetGamesInventaryProfileFilterDto
{
    public bool? ShirtsRequired { get; set; }

    public RangeLimitDto<int?>? ShirtsCount { get; set; }
}