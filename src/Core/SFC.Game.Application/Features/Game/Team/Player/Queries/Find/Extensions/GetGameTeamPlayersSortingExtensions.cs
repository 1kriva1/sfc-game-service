using System.Linq.Expressions;

using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Extensions;
public static class GetGameTeamPlayersSortingExtensions
{
    public static IEnumerable<Sorting<GameTeamPlayer, dynamic>> BuildGameTeamPlayerSorting(this IEnumerable<SortingDto> sorting)
        => sorting.BuildSearchSorting(BuildGameTeamSortingExpression);

    private static Expression<Func<GameTeamPlayer, dynamic>>? BuildGameTeamSortingExpression(string name)
    {
        return name switch
        {
            $"{nameof(GetGameTeamPlayersFilterDto.GameTeamPlayer)}.{nameof(GetGameTeamPlayersGameTeamPlayerFilterDto.Statuses)}" => p => p.StatusId,
            $"{nameof(GetGameTeamPlayersFilterDto.Player)}.{nameof(PlayerGeneralProfile.FirstName)}" => p => p.Player.GeneralProfile.FirstName,
            $"{nameof(GetGameTeamPlayersFilterDto.Player)}.{nameof(PlayerGeneralProfile.LastName)}" => p => p.Player.GeneralProfile.LastName,
            $"{nameof(GetGameTeamPlayersFilterDto.Player)}.{nameof(PlayerFootballProfile.PhysicalCondition)}" => p => p.Player.FootballProfile.PhysicalCondition!,
            $"{nameof(GetGameTeamPlayersFilterDto.Player)}.{nameof(PlayerFootballProfile.Height)}" => p => p.Player.FootballProfile.Height!,
            $"{nameof(GetGameTeamPlayersFilterDto.Player)}.{nameof(PlayerFootballProfile.Weight)}" => p => p.Player.FootballProfile.Weight!,
            $"{nameof(GetGameTeamPlayersFilterDto.Player)}.{nameof(PlayerStatsFilterDto.Raiting)}" => p => p.Player.Stats.Sum(m => m.Value),
            _ => p => p.StatusId
        };
    }
}