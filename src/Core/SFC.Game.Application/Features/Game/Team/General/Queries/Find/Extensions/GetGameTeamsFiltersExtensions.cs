using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Find.Extensions;
public static class GetGameTeamsFiltersExtensions
{
    public static IEnumerable<Filter<GameTeam>> BuildSearchFilters(this GetGameTeamsFilterDto filter, DateTime now)
    {
        return [
            // permanent
            new()
            {
                Condition = true,
                Expression = gameTeam => gameTeam.GameId == filter!.GameId
            },
            // game team
            new()
            {
                Condition = filter?.GameTeam?.Statuses?.Any() ?? false,
                Expression = gameTeam => filter!.GameTeam!.Statuses!.Contains((int)gameTeam.StatusId)
            },
            // team
            new()
            {
                Condition = filter?.Team?.Statuses?.Any() ?? false,
                Expression = gameTeam => filter!.Team!.Statuses!.Contains((int)gameTeam.StatusId)
            },
            // team general profile
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Team?.Profile?.General?.Name),
                Expression = gameTeam => gameTeam.Team.GeneralProfile.Name.Contains(filter!.Team!.Profile!.General!.Name!)
            },
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Team?.Profile?.General?.City),
                Expression = gameTeam => gameTeam.Team.GeneralProfile.City.Contains(filter!.Team!.Profile!.General!.City!)
            },
            new()
            {
                Condition = filter?.Team?.Profile?.General?.Tags?.Any() ?? false,
                Expression = gameTeam => gameTeam.Team.Tags.Any(tag => filter!.Team!.Profile!.General!.Tags!.Contains(tag.Value))
            },
            new()
            {
                Condition = (filter?.Team?.Profile?.General?.Availability?.From.HasValue ?? false)
                    && (filter.Team.Profile.General?.Availability!.To == null || filter.Team.Profile.General.Availability.From <= filter.Team.Profile.General.Availability.To),
                Expression = gameTeam => gameTeam.Team.Availability.Any(availability => availability.From <= filter!.Team!.Profile!.General!.Availability!.From
                    && filter!.Team!.Profile!.General!.Availability!.Days.Contains(availability.Day))
            },
            new()
            {
                Condition = (filter?.Team?.Profile?.General?.Availability?.To.HasValue ?? false)
                    && (filter.Team.Profile.General.Availability.From == null || filter.Team.Profile.General.Availability.To >= filter.Team.Profile.General.Availability.From),
                Expression = gameTeam => gameTeam.Team.Availability.Any(availability => availability.To >= filter!.Team!.Profile!.General!.Availability!.To
                    && filter!.Team!.Profile!.General!.Availability!.Days.Contains(availability.Day))
            },
            new()
            {
                Condition = filter?.Team?.Profile?.General?.Availability?.Days?.Any() ?? false,
                Expression = gameTeam => gameTeam.Team.Availability.Any(availability => filter!.Team!.Profile!.General!.Availability!.Days.Contains(availability.Day))
            },
            new()
            {
                Condition = filter?.Team?.Profile?.General?.HasLogo.HasValue ?? false,
                Expression = gameTeam => filter!.Team!.Profile!.General!.HasLogo!.Value && gameTeam.Team.Logo != null && gameTeam.Team.Logo.Size > 0
                    || !filter.Team.Profile.General!.HasLogo!.Value && (gameTeam.Team.Logo == null || gameTeam.Team.Logo.Size <= 0)
            },
            new()
            {
                Condition = filter?.Team?.Profile?.General?.LocationId.HasValue ?? false,
                Expression = gameTeam => gameTeam.Team.GeneralProfile.LocationId == filter!.Team!.Profile!.General!.LocationId
            },
            // team financial profile
            new()
            {
                Condition = filter?.Team?.Profile?.Financial?.FreePlay.HasValue ?? false,
                Expression = gameTeam => gameTeam.Team.FinancialProfile.FreePlay == filter!.Team!.Profile!.Financial!.FreePlay
            },
            // team inventary profile
            new()
            {
                Condition = filter?.Team?.Profile?.Inventary?.Shirts?.Any() ?? false,
                Expression = gameTeam => gameTeam.Team.Shirts.Any(shirt => filter!.Team!.Profile!.Inventary!.Shirts.Contains((int)shirt.ShirtId))
            }
        ];
    }
}