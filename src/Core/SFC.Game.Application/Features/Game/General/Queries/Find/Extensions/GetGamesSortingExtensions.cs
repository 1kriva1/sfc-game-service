using System.Linq.Expressions;

using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Application.Features.Game.General.Queries.Find.Extensions;
public static class GetGamesSortingExtensions
{
    public static IEnumerable<Sorting<GameEntity, dynamic>> BuildGameSearchSorting(this IEnumerable<SortingDto> sorting)
        => sorting.BuildSearchSorting<GameEntity>(BuildExpression);

    private static Expression<Func<GameEntity, dynamic>>? BuildExpression(string name)
    {
        return name switch
        {
            nameof(GameGeneralProfile.Name) => p => p.GeneralProfile.Name,
            nameof(GetGamesFilterDto.Statuses) => p => p.StatusId,
            _ => null
        };
    }
}