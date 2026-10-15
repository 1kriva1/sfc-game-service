using System.Linq.Expressions;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Features.Common.Constants;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Extensions;

public static class GetGameTeamPlayersFiltersExtensions
{
    public static IEnumerable<Filter<GameTeamPlayer>> BuildSearchFilters(this GetGameTeamPlayersFilterDto filter, DateTime now)
    {
        return [
            // permanent
            new()
            {
                Condition = true,
                Expression = gameTeamPlayer => gameTeamPlayer.GameTeam.GameId == filter!.GameId
            },
            new()
            {
                Condition = true,
                Expression = gameTeamPlayer => gameTeamPlayer.GameTeam.TeamId == filter!.TeamId
            }, 
            // game team player
            new()
            {
                Condition = filter?.GameTeamPlayer?.ExcludeIds?.Any() ?? false,
                Expression = gameTeamPlayer => !filter!.GameTeamPlayer!.ExcludeIds!.Contains(gameTeamPlayer.Id)
            },           
            // player
            new()
            {
                Condition = filter?.Player?.ExcludeIds?.Any() ?? false,
                Expression = gameTeamPlayer => (gameTeamPlayer.GamePlayer == null || !filter!.Player!.ExcludeIds!.Contains(gameTeamPlayer.GamePlayer.PlayerId))
                    && (gameTeamPlayer.TeamPlayer == null || !filter!.Player!.ExcludeIds!.Contains(gameTeamPlayer.TeamPlayer.PlayerId))
            },
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Player?.Profile?.General?.Name),
                Expression = gameTeamPlayer =>
                    gameTeamPlayer.GamePlayer!.Player.GeneralProfile.FirstName.Contains(filter!.Player!.Profile.General!.Name!) ||
                    gameTeamPlayer.TeamPlayer!.Player.GeneralProfile.FirstName.Contains(filter!.Player!.Profile.General!.Name!) ||
                    gameTeamPlayer.GamePlayer!.Player.GeneralProfile.LastName.Contains(filter.Player.Profile.General.Name!) ||
                    gameTeamPlayer.TeamPlayer!.Player.GeneralProfile.LastName.Contains(filter.Player.Profile.General.Name!)
            },
            new()
            {
                Condition = !string.IsNullOrEmpty(filter?.Player?.Profile?.General?.City),
                Expression = gameTeamPlayer =>
                                    gameTeamPlayer.GamePlayer!.Player.GeneralProfile.City.Contains(filter!.Player!.Profile.General!.City!) ||
                                    gameTeamPlayer.TeamPlayer!.Player.GeneralProfile.City.Contains(filter!.Player!.Profile.General!.City!)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Tags?.Any() ?? false,
                Expression = gameTeamPlayer =>
                                    gameTeamPlayer.GamePlayer!.Player.Tags.Any(tag => filter!.Player!.Profile.General!.Tags.Contains(tag.Value)) ||
                                    gameTeamPlayer.TeamPlayer!.Player.Tags.Any(tag => filter!.Player!.Profile.General!.Tags.Contains(tag.Value))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Years.BuildLimitFromCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.GeneralProfile.Birthday.HasValue || gameTeamPlayer.GamePlayer.Player.GeneralProfile.Birthday <= now.AddYears(-filter!.Player!.Profile.General!.Years!.From!.Value))) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.GeneralProfile.Birthday.HasValue || gameTeamPlayer.TeamPlayer.Player.GeneralProfile.Birthday <= now.AddYears(-filter!.Player!.Profile.General!.Years!.From!.Value)))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Years.BuildLimitToCondition(ValidationConstants.RangeLimit) ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.GeneralProfile.Birthday.HasValue || gameTeamPlayer.GamePlayer.Player.GeneralProfile.Birthday >= now.AddYears(-filter!.Player!.Profile.General!.Years!.To!.Value))) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.GeneralProfile.Birthday.HasValue || gameTeamPlayer.TeamPlayer.Player.GeneralProfile.Birthday >= now.AddYears(-filter!.Player!.Profile.General!.Years!.To!.Value)))
            },
            new()
            {
                Condition = (filter?.Player?.Profile?.General?.Availability?.From.HasValue ?? false)
                    && (filter.Player.Profile.General?.Availability!.To == null || filter.Player.Profile.General.Availability.From <= filter.Player.Profile.General.Availability.To),
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.Availability.From.HasValue || gameTeamPlayer.GamePlayer.Player.Availability.From <= filter!.Player!.Profile.General!.Availability.From)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.Availability.From.HasValue || gameTeamPlayer.TeamPlayer.Player.Availability.From <= filter!.Player!.Profile.General!.Availability.From))
            },
            new()
            {
                Condition = (filter?.Player?.Profile?.General?.Availability?.To.HasValue ?? false)
                    && (filter.Player.Profile.General.Availability.From == null || filter.Player.Profile.General.Availability.To >= filter.Player.Profile.General.Availability.From),
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.Availability.To.HasValue || gameTeamPlayer.GamePlayer.Player.Availability.To >= filter!.Player!.Profile.General!.Availability.To)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.Availability.To.HasValue || gameTeamPlayer.TeamPlayer.Player.Availability.To >= filter!.Player!.Profile.General!.Availability.To))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.Availability?.Days?.Any() ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (gameTeamPlayer.GamePlayer.Player.Availability.Days.Count == 0 || gameTeamPlayer.GamePlayer.Player.Availability.Days.Any(day => filter!.Player!.Profile.General!.Availability.Days.Contains(day.Day)))) ||
                                    (gameTeamPlayer.TeamPlayer != null && (gameTeamPlayer.TeamPlayer.Player.Availability.Days.Count == 0 || gameTeamPlayer.TeamPlayer.Player.Availability.Days.Any(day => filter!.Player!.Profile.General!.Availability.Days.Contains(day.Day))))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.FreePlay.HasValue ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && gameTeamPlayer.GamePlayer.Player.GeneralProfile.FreePlay == filter!.Player!.Profile.General!.FreePlay) ||
                                    (gameTeamPlayer.TeamPlayer != null && gameTeamPlayer.TeamPlayer.Player.GeneralProfile.FreePlay == filter!.Player!.Profile.General!.FreePlay)
            },
            new()
            {
                Condition = filter?.Player?.Profile?.General?.HasPhoto.HasValue ?? false,
                Expression = gameTeamPlayer =>
                    (gameTeamPlayer.GamePlayer != null && (filter!.Player!.Profile.General!.HasPhoto!.Value && gameTeamPlayer.GamePlayer.Player.Photo != null && gameTeamPlayer.GamePlayer.Player.Photo.Size > 0
                        || !filter.Player.Profile.General!.HasPhoto!.Value && (gameTeamPlayer.GamePlayer.Player.Photo == null || gameTeamPlayer.GamePlayer.Player.Photo.Size <= 0))) ||
                    (gameTeamPlayer.TeamPlayer != null && (filter!.Player!.Profile.General!.HasPhoto!.Value && gameTeamPlayer.TeamPlayer.Player.Photo != null && gameTeamPlayer.TeamPlayer.Player.Photo.Size > 0
                        || !filter.Player.Profile.General!.HasPhoto!.Value && (gameTeamPlayer.TeamPlayer.Player.Photo == null || gameTeamPlayer.TeamPlayer.Player.Photo.Size <= 0)))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Height.BuildLimitFromCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.Height.HasValue || gameTeamPlayer.GamePlayer.Player.FootballProfile.Height >= filter!.Player!.Profile.Football!.Height.From)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.Height.HasValue || gameTeamPlayer.TeamPlayer.Player.FootballProfile.Height >= filter!.Player!.Profile.Football!.Height.From))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Height.BuildLimitToCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.Height.HasValue || gameTeamPlayer.GamePlayer.Player.FootballProfile.Height <= filter!.Player!.Profile.Football!.Height.To)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.Height.HasValue || gameTeamPlayer.TeamPlayer.Player.FootballProfile.Height <= filter!.Player!.Profile.Football!.Height.To))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Weight.BuildLimitFromCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.Weight.HasValue || gameTeamPlayer.GamePlayer.Player.FootballProfile.Weight >= filter!.Player!.Profile.Football!.Weight.From)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.Weight.HasValue || gameTeamPlayer.TeamPlayer.Player.FootballProfile.Weight >= filter!.Player!.Profile.Football!.Weight.From))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Weight.BuildLimitToCondition(ValidationConstants.PlayerSizeRange) ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.Weight.HasValue || gameTeamPlayer.GamePlayer.Player.FootballProfile.Weight <= filter!.Player!.Profile.Football!.Weight.To)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.Weight.HasValue || gameTeamPlayer.TeamPlayer.Player.FootballProfile.Weight <= filter!.Player!.Profile.Football!.Weight.To))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Positions?.Any() ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.PositionId.HasValue || filter!.Player!.Profile.Football!.Positions.Contains((int)gameTeamPlayer.GamePlayer.Player.FootballProfile.PositionId!.Value))) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.PositionId.HasValue || filter!.Player!.Profile.Football!.Positions.Contains((int)gameTeamPlayer.TeamPlayer.Player.FootballProfile.PositionId!.Value)))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.WorkingFoot.HasValue ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.WorkingFootId.HasValue || (int?)gameTeamPlayer.GamePlayer.Player.FootballProfile.WorkingFootId == filter!.Player!.Profile.Football!.WorkingFoot)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.WorkingFootId.HasValue || (int?)gameTeamPlayer.TeamPlayer.Player.FootballProfile.WorkingFootId == filter!.Player!.Profile.Football!.WorkingFoot))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.GameStyles?.Any() ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.GameStyleId.HasValue || filter!.Player!.Profile.Football!.GameStyles.Contains((int)gameTeamPlayer.GamePlayer.Player.FootballProfile.GameStyleId!.Value))) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.GameStyleId.HasValue || filter!.Player!.Profile.Football!.GameStyles.Contains((int)gameTeamPlayer.TeamPlayer.Player.FootballProfile.GameStyleId!.Value)))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.Skill.HasValue ?? false,
                Expression = gameTeamPlayer =>
                                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.Skill.HasValue || gameTeamPlayer.GamePlayer.Player.FootballProfile.Skill >= filter!.Player!.Profile.Football!.Skill)) ||
                                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.Skill.HasValue || gameTeamPlayer.TeamPlayer.Player.FootballProfile.Skill >= filter!.Player!.Profile.Football!.Skill))
            },
            new()
            {
                Condition = filter?.Player?.Profile?.Football?.PhysicalCondition.HasValue ?? false,
                Expression = gameTeamPlayer =>
                    (gameTeamPlayer.GamePlayer != null && (!gameTeamPlayer.GamePlayer.Player.FootballProfile.PhysicalCondition.HasValue || gameTeamPlayer.GamePlayer.Player.FootballProfile.PhysicalCondition >= filter!.Player!.Profile.Football!.PhysicalCondition)) ||
                    (gameTeamPlayer.TeamPlayer != null && (!gameTeamPlayer.TeamPlayer.Player.FootballProfile.PhysicalCondition.HasValue || gameTeamPlayer.TeamPlayer.Player.FootballProfile.PhysicalCondition >= filter!.Player!.Profile.Football!.PhysicalCondition))
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

    private static Expression<Func<GameTeamPlayer, bool>> FilterByStat(int? skill, short? from, short? to)
    {
        if (from.HasValue)
        {
            return gameTeamPlayer =>
                (gameTeamPlayer.GamePlayer != null && (int)Math.Ceiling((double)gameTeamPlayer.GamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                    / (gameTeamPlayer.GamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                    * ValidationConstants.PercentageMaxValue) >= from) ||
                (gameTeamPlayer.TeamPlayer != null && (int)Math.Ceiling((double)gameTeamPlayer.TeamPlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                    / (gameTeamPlayer.TeamPlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                    * ValidationConstants.PercentageMaxValue) >= from);
        }
        else if (to.HasValue)
        {
            return gameTeamPlayer =>
                (gameTeamPlayer.GamePlayer != null && (int)Math.Ceiling((double)gameTeamPlayer.GamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                    / (gameTeamPlayer.GamePlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                    * ValidationConstants.PercentageMaxValue) <= to) ||
                (gameTeamPlayer.TeamPlayer != null && (int)Math.Ceiling((double)gameTeamPlayer.TeamPlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Sum(m => m.Value)
                    / (gameTeamPlayer.TeamPlayer.Player.Stats.Where(s => !skill.HasValue || (int?)s.Type.SkillId == skill).Count() * PlayerConstants.StatMaxValue)
                    * ValidationConstants.PercentageMaxValue) <= to);
        }

        return player => true;
    }

    private static Expression<Func<GameTeamPlayer, bool>> FilterByRaiting(int? raiting)
    {
        return raiting.HasValue
            ? gameTeamPlayer =>
                (gameTeamPlayer.GamePlayer != null && PlayerConstants.StarsMaxValue * (int)Math.Ceiling((double)gameTeamPlayer.GamePlayer.Player.Stats.Sum(m => m.Value)
                    / (gameTeamPlayer.GamePlayer.Player.Stats.Count() * PlayerConstants.StatMaxValue) * ValidationConstants.PercentageMaxValue)
                    / ValidationConstants.PercentageMaxValue >= raiting) ||
                (gameTeamPlayer.TeamPlayer != null && PlayerConstants.StarsMaxValue * (int)Math.Ceiling((double)gameTeamPlayer.TeamPlayer.Player.Stats.Sum(m => m.Value)
                    / (gameTeamPlayer.TeamPlayer.Player.Stats.Count() * PlayerConstants.StatMaxValue) * ValidationConstants.PercentageMaxValue)
                    / ValidationConstants.PercentageMaxValue >= raiting)
            : gameTeamPlayer => true;
    }

    #endregion Private
}