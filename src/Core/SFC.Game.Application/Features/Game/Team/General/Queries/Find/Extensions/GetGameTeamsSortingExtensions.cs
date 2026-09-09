using System.Linq.Expressions;

using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Find.Extensions;
public static class GetGameTeamsSortingExtensions
{
    public static IEnumerable<Sorting<GameTeam, dynamic>> BuildGameTeamSorting(this IEnumerable<SortingDto> sorting)
        => sorting.BuildSearchSorting(BuildGameTeamSortingExpression);

    private static Expression<Func<GameTeam, dynamic>>? BuildGameTeamSortingExpression(string name)
    {
        return name switch
        {
            $"{nameof(GetGameTeamsFilterDto.GameTeam)}.{nameof(GetGameTeamsGameTeamFilterDto.Statuses)}" => p => p.StatusId,
            $"{nameof(GetGameTeamsFilterDto.Team)}.{nameof(TeamGeneralProfile.Name)}" => p => p.Team.GeneralProfile.Name,
            $"{nameof(GetGameTeamsFilterDto.Team)}.{nameof(TeamGeneralProfile.City)}" => p => p.Team.GeneralProfile.City,
            _ => null
        };
    }
}