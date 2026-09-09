using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.General.Queries.Find.Extensions;

public static class GetGamesFiltersExtensions
{
    public static IEnumerable<Filter<GameEntity>> BuildSearchFilters(this GetGamesFilterDto filter)
    {
        return [
            new()
            {
                Condition = filter?.Statuses?.Any() ?? false,
                Expression = game => filter!.Statuses!.Contains((int)game.StatusId)
            },
            // general profile
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Profile?.General?.Name),
                Expression = game => game.GeneralProfile.Name.Contains(filter!.Profile!.General!.Name!)
            },
            new()
            {
                Condition = filter?.Profile?.General?.Availability?.Date.HasValue ?? false,
                Expression = game => game.Availability.Date == filter!.Profile!.General!.Availability!.Date
            },
            new()
            {
                Condition = (filter?.Profile?.General?.Availability?.From.HasValue ?? false),
                Expression = game => game.Availability.From >= filter!.Profile!.General!.Availability!.From
            },
            new()
            {
                Condition = (filter?.Profile?.General?.Availability?.To.HasValue ?? false),
                Expression = game => game.Availability.To <= filter!.Profile!.General!.Availability!.To
            },
            new()
            {
                Condition = filter?.Profile?.General?.Tags?.Any() ?? false,
                Expression = game => game.Tags.Any(tag => filter!.Profile!.General!.Tags!.Contains(tag.Value))
            },
            new()
            {
                Condition = filter?.Profile?.General?.LocationId.HasValue ?? false,
                Expression = game => game.GeneralProfile.LocationId == filter!.Profile!.General!.LocationId
            },
            // financial profile
            new()
            {
                Condition = filter?.Profile?.Financial?.FreeGame.HasValue ?? false,
                Expression = game => game.FinancialProfile.FreeGame == filter!.Profile!.Financial!.FreeGame
            },
            new()
            {
                Condition = filter?.Profile?.Financial?.PayAmount?.From.HasValue ?? false,
                Expression = game => !game.FinancialProfile.PayAmount.HasValue ||
                                      game.FinancialProfile.PayAmount >= filter!.Profile!.Financial!.PayAmount!.From
            },
            new()
            {
                Condition = filter?.Profile?.Financial?.PayAmount?.To.HasValue ?? false,
                Expression = game => !game.FinancialProfile.PayAmount.HasValue ||
                                      game.FinancialProfile.PayAmount <= filter!.Profile!.Financial!.PayAmount!.To
            },
            // inventary profile
            new()
            {
                Condition = filter?.Profile?.Inventary?.ShirtsRequired.HasValue ?? false,
                Expression = game => game.InventaryProfile.ShirtsRequired == filter!.Profile!.Inventary!.ShirtsRequired
            },
            new()
            {
                Condition = filter?.Profile?.Inventary?.ShirtsCount?.From.HasValue ?? false,
                Expression = game => !game.InventaryProfile.ShirtsCount.HasValue ||
                                     game.InventaryProfile.ShirtsCount >= filter!.Profile!.Inventary!.ShirtsCount!.From
            },
            new()
            {
                Condition = filter?.Profile?.Inventary?.ShirtsCount?.To.HasValue ?? false,
                Expression = game => !game.InventaryProfile.ShirtsCount.HasValue ||
                                     game.InventaryProfile.ShirtsCount <= filter!.Profile!.Inventary!.ShirtsCount!.To
            },
        ];
    }
}