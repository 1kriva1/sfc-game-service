using System.Linq.Expressions;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Features.Common.Constants;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Queries.Find.Extensions;

public static class GetGamePlayersFiltersExtensions
{
    public static IEnumerable<Filter<GamePlayer>> BuildSearchFilters(this GetGamePlayersFilterDto filter, DateTime now)
    {
        return [
            // permanent
            new()
            {
                Condition = true,
                Expression = gamePlayer => gamePlayer.GameId == filter!.GameId
            },
            // game player           
            new()
            {
                Condition = filter?.GamePlayer?.Statuses?.Any() ?? false,
                Expression = gamePlayer => filter!.GamePlayer!.Statuses!.Contains((int)gamePlayer.StatusId)
            },
            // player
            new()
            {
                Condition = filter?.Player?.ExcludeIds?.Any() ?? false,
                Expression = gamePlayer => !filter!.Player!.ExcludeIds!.Contains((int)gamePlayer.PlayerId)
            },
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Player?.Profile?.General?.Name),
                Expression = gamePlayer => gamePlayer.Player.GeneralProfile.FirstName.Contains(filter!.Player!.Profile.General!.Name!)
                    || gamePlayer.Player.GeneralProfile.LastName.Contains(filter.Player.Profile.General.Name!)
            },
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Player?.Profile?.General?.City),
                Expression = gamePlayer => gamePlayer.Player.GeneralProfile.City.Contains(filter!.Player!.Profile.General!.City!)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Tags?.Any() ?? false,
                Expression = gamePlayer => gamePlayer.Player.Tags.Any(tag => filter!.Player!.Profile.General!.Tags.Contains(tag.Value))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Years.BuildLimitFromCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = gamePlayer => !gamePlayer.Player.GeneralProfile.Birthday.HasValue || gamePlayer.Player.GeneralProfile.Birthday <= now.AddYears(-filter!.Player!.Profile.General!.Years!.From!.Value)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Years.BuildLimitToCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = gamePlayer => !gamePlayer.Player.GeneralProfile.Birthday.HasValue || gamePlayer.Player.GeneralProfile.Birthday >= now.AddYears(-filter!.Player!.Profile.General!.Years!.To!.Value)
            },
            new()
            {
                Condition = (filter?.Player?.Profile?.General?.Availability?.From.HasValue ?? false)
                    && (filter.Player.Profile.General?.Availability!.To == null || filter.Player.Profile.General.Availability.From <= filter.Player.Profile.General.Availability.To),
                Expression = gamePlayer => !gamePlayer.Player.Availability.From.HasValue || gamePlayer.Player.Availability.From <= filter!.Player!.Profile.General!.Availability.From
            },
            new()
            {
                Condition = (filter?.Player?.Profile?.General?.Availability?.To.HasValue ?? false)
                    && (filter.Player.Profile.General.Availability.From == null || filter.Player.Profile.General.Availability.To >= filter.Player.Profile.General.Availability.From),
                Expression = gamePlayer => !gamePlayer.Player.Availability.To.HasValue || gamePlayer.Player.Availability.To >= filter!.Player!.Profile.General!.Availability.To
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Availability?.Days?.Any() ?? false,
                Expression = gamePlayer => gamePlayer.Player.Availability.Days.Count == 0 || gamePlayer.Player.Availability.Days.Any(day => filter!.Player!.Profile.General!.Availability.Days.Contains(day.Day))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.FreePlay.HasValue ?? false,
                Expression = gamePlayer => gamePlayer.Player.GeneralProfile.FreePlay == filter!.Player!.Profile.General!.FreePlay
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.HasPhoto.HasValue ?? false,
                Expression = gamePlayer => filter!.Player!.Profile.General!.HasPhoto!.Value && gamePlayer.Player.Photo != null && gamePlayer.Player.Photo.Size > 0
                    || !filter.Player.Profile.General!.HasPhoto!.Value && (gamePlayer.Player.Photo == null || gamePlayer.Player.Photo.Size <= 0)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Height.BuildLimitFromCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.Height.HasValue || gamePlayer.Player.FootballProfile.Height >= filter!.Player!.Profile.Football!.Height.From
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Height.BuildLimitToCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.Height.HasValue || gamePlayer.Player.FootballProfile.Height <= filter!.Player!.Profile.Football!.Height.To
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Weight.BuildLimitFromCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.Weight.HasValue || gamePlayer.Player.FootballProfile.Weight >= filter!.Player!.Profile.Football!.Weight.From
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Weight.BuildLimitToCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.Weight.HasValue || gamePlayer.Player.FootballProfile.Weight <= filter!.Player!.Profile.Football!.Weight.To
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Positions?.Any() ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.PositionId.HasValue || filter!.Player!.Profile.Football!.Positions.Contains((int)gamePlayer.Player.FootballProfile.PositionId.Value)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.WorkingFoot.HasValue ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.WorkingFootId.HasValue || (int?)gamePlayer.Player.FootballProfile.WorkingFootId == filter!.Player!.Profile.Football!.WorkingFoot
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.GameStyles?.Any() ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.GameStyleId.HasValue || filter!.Player!.Profile.Football!.GameStyles.Contains((int)gamePlayer.Player.FootballProfile.GameStyleId.Value)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Skill.HasValue ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.Skill.HasValue || gamePlayer.Player.FootballProfile.Skill >= filter!.Player!.Profile.Football!.Skill
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.PhysicalCondition.HasValue ?? false,
                Expression = gamePlayer => !gamePlayer.Player.FootballProfile.PhysicalCondition.HasValue
                    || gamePlayer.Player.FootballProfile.PhysicalCondition >= filter!.Player!.Profile.Football!.PhysicalCondition
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Total.BuildLimitFromCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(null, filter?.Player?.Stats?.Total?.From, null)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Total.BuildLimitToCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(null, null, filter?.Player?.Stats?.Total?.To)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Physical.BuildLimitFromCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(filter?.Player?.Stats?.Physical?.Skill, filter?.Player?.Stats?.Physical?.From, null)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Physical.BuildLimitToCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(filter?.Player?.Stats?.Physical?.Skill, null, filter?.Player?.Stats?.Physical?.To)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Mental.BuildLimitFromCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(filter?.Player?.Stats?.Mental?.Skill, filter?.Player?.Stats?.Mental?.From, null)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Mental.BuildLimitToCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(filter?.Player?.Stats?.Mental?.Skill, null, filter?.Player?.Stats?.Mental?.To)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Skill.BuildLimitFromCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(filter?.Player?.Stats?.Skill?.Skill, filter?.Player?.Stats?.Skill?.From, null)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Skill.BuildLimitToCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = FilterByStat(filter?.Player?.Stats?.Skill?.Skill, null, filter?.Player?.Stats?.Skill?.To)
            },
            new()
            {
                Condition = filter?.Player?.Stats?.Raiting.HasValue ?? false,
                Expression = FilterByRaiting(filter?.Player?.Stats?.Raiting)
            }
        ];
    }

    #region Private

    private static Expression<Func<GamePlayer, bool>> FilterByStat(int? skill, short? from, short? to)
    {
        if (from.HasValue)
        {
            return gamePlayer => (int)Math.Ceiling((double)gamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                / (gamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                * ValidationConstants.PercentageMaxValue) >= from;
        }
        else if (to.HasValue)
        {
            return gamePlayer => (int)Math.Ceiling((double)gamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                / (gamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                * ValidationConstants.PercentageMaxValue) <= to;
        }

        return player => true;
    }

    private static Expression<Func<GamePlayer, bool>> FilterByRaiting(int? raiting)
    {
        return raiting.HasValue
            ? gamePlayer => PlayerConstants.StarsMaxValue * (int)Math.Ceiling((double)gamePlayer.Player.Stats.Sum(m => m.Value)
                / (gamePlayer.Player.Stats.Count() * PlayerConstants.StatMaxValue) * ValidationConstants.PercentageMaxValue)
                / ValidationConstants.PercentageMaxValue >= raiting
            : gamePlayer => true;
    }

    #endregion Private
}