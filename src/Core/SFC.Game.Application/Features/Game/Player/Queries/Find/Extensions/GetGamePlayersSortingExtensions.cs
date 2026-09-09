using System.Linq.Expressions;

using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Player;

namespace SFC.Game.Application.Features.Game.Player.Queries.Find.Extensions;
public static class GetGamePlayersSortingExtensions
{
    public static IEnumerable<Sorting<GamePlayer, dynamic>> BuildGamePlayerSorting(this IEnumerable<SortingDto> sorting)
        => sorting.BuildSearchSorting(BuildGamePlayerSortingExpression);

    private static Expression<Func<GamePlayer, dynamic>>? BuildGamePlayerSortingExpression(string name)
    {
        return name switch
        {
            $"{nameof(GetGamePlayersFilterDto.GamePlayer)}.{nameof(GetGamePlayersGamePlayerFilterDto.Statuses)}" => p => p.StatusId,
            $"{nameof(GetGamePlayersFilterDto.Player)}.{nameof(PlayerGeneralProfile.FirstName)}" => p => p.Player.GeneralProfile.FirstName,
            $"{nameof(GetGamePlayersFilterDto.Player)}.{nameof(PlayerGeneralProfile.LastName)}" => p => p.Player.GeneralProfile.LastName,
            $"{nameof(GetGamePlayersFilterDto.Player)}.{nameof(PlayerFootballProfile.PhysicalCondition)}" => p => p.Player.FootballProfile.PhysicalCondition!,
            $"{nameof(GetGamePlayersFilterDto.Player)}.{nameof(PlayerFootballProfile.Height)}" => p => p.Player.FootballProfile.Height!,
            $"{nameof(GetGamePlayersFilterDto.Player)}.{nameof(PlayerFootballProfile.Weight)}" => p => p.Player.FootballProfile.Weight!,
            $"{nameof(GetGamePlayersFilterDto.Player)}.{nameof(PlayerStatsFilterDto.Raiting)}" => p => p.Player.Stats.Sum(m => m.Value),
            _ => p => p.StatusId
        };
    }
}