using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Common.Dto.Common;

namespace SFC.Game.Application.Features.Common.Validators.Player;
public class PlayerFilterValidator : AbstractValidator<PlayerFilterDto>
{
    public PlayerFilterValidator()
    {
        When(p => p?.Profile?.General != null, () =>
        {
            RuleFor(p => p.Profile.General.Name)
                .MaximumLength(ValidationConstants.NameValueMaxLength)
                .WithName(nameof(PlayerGeneralProfileFilterDto.Name));

            RuleFor(p => p.Profile.General.City)
                 .MaximumLength(ValidationConstants.CityValueMaxLength)
                 .WithName(nameof(PlayerGeneralProfileFilterDto.City));

            When(p => p.Profile.General.Tags?.Any() ?? false, () =>
            {
                RuleFor(p => p.Profile.General.Tags)
                    .Must(tags => tags.Distinct().Count() == tags.Count())
                    .WithMessage(Localization.MustBeUnique)
                    .Must(tags => tags.Count() <= ValidationConstants.TagsMaxLength)
                    .WithName(nameof(PlayerGeneralProfileFilterDto.Tags))
                    .WithMessage(Localization.MustNotExceedSize.BuildValidationMessage(nameof(PlayerGeneralProfileFilterDto.Tags), ValidationConstants.TagsMaxLength));

                RuleForEach(p => p.Profile.General.Tags)
                    .NotEmpty()
                    .WithName(nameof(PlayerGeneralProfileFilterDto.Tags))
                    .WithMessage(Localization.MustNotBeEmpty)
                    .MaximumLength(ValidationConstants.TagValueMaxLength)
                    .WithMessage(Localization.MustNotExceedCharactersSize);
            });

            When(p => p.Profile.General.Availability?.Days?.Any() ?? false, () =>
            {
                RuleFor(p => p.Profile.General.Availability.Days)
                    .Must(days => days.Count() <= Enum.GetNames(typeof(DayOfWeek)).Length)
                    .WithMessage(Localization.MustNotExceedSize.BuildValidationMessage(nameof(PlayerAvailabilityLimitFilterDto.Days), Enum.GetNames(typeof(DayOfWeek)).Length));

                RuleForEach(p => p.Profile.General.Availability.Days)
                    .IsInEnum()
                    .WithName(nameof(PlayerAvailabilityLimitFilterDto.Days))
                    .WithMessage(Localization.InvalidDaysOfWeek);

            });

            When(p => (p.Profile.General.Availability?.From.HasValue ?? false)
                && (p.Profile.General.Availability?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Profile.General.Availability.To)
                        .GreaterThanOrEqualTo(p => p.Profile.General.Availability.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(PlayerAvailabilityLimitFilterDto.To), nameof(PlayerAvailabilityLimitFilterDto.From)));

                    RuleFor(p => p.Profile.General.Availability.From)
                        .LessThanOrEqualTo(p => p.Profile.General.Availability.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(PlayerAvailabilityLimitFilterDto.From), nameof(PlayerAvailabilityLimitFilterDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Profile.General.Years?.From.HasValue ?? false)
                && (p.Profile.General.Years?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Profile.General.Years.To)
                        .GreaterThanOrEqualTo(p => p.Profile.General.Years.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                    RuleFor(p => p.Profile.General.Years.From)
                        .LessThanOrEqualTo(p => p.Profile.General.Years.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });

        When(p => p.Profile?.Football != null, () =>
        {
            When(p => (p.Profile.Football.Height?.From.HasValue ?? false)
            && (p.Profile.Football.Height?.To.HasValue ?? false), () =>
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                RuleFor(p => p.Profile.Football.Height.To)
                    .GreaterThanOrEqualTo(p => p.Profile.Football.Height.From.Value)
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                RuleFor(p => p.Profile.Football.Height.From)
                    .LessThanOrEqualTo(p => p.Profile.Football.Height.To.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });

            When(p => (p.Profile.Football.Weight?.From.HasValue ?? false)
                && (p.Profile.Football.Weight?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Profile.Football.Weight.To)
                        .GreaterThanOrEqualTo(p => p.Profile.Football.Weight.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                    RuleFor(p => p.Profile.Football.Weight.From)
                        .LessThanOrEqualTo(p => p.Profile.Football.Weight.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });

        When(p => p.Stats != null, () =>
        {
            When(p => (p.Stats.Total?.From.HasValue ?? false)
            && (p.Stats.Total?.To.HasValue ?? false), () =>
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                RuleFor(p => p.Stats.Total.To)
                    .GreaterThanOrEqualTo(p => p.Stats.Total.From.Value)
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.To), nameof(RangeLimitDto<short?>.From)));

                RuleFor(p => p.Stats.Total.From)
                    .LessThanOrEqualTo(p => p.Stats.Total.To.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<short?>.From), nameof(RangeLimitDto<short?>.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
            });

            When(p => (p.Stats.Physical?.From.HasValue ?? false)
                && (p.Stats.Physical?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Stats.Physical.To)
                        .GreaterThanOrEqualTo(p => p.Stats.Physical.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(PlayerStatsBySkillRangeLimitFilterDto.To), nameof(PlayerStatsBySkillRangeLimitFilterDto.From)));

                    RuleFor(p => p.Stats.Physical.From)
                        .LessThanOrEqualTo(p => p.Stats.Physical.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(PlayerStatsBySkillRangeLimitFilterDto.From), nameof(PlayerStatsBySkillRangeLimitFilterDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Stats.Mental?.From.HasValue ?? false)
                && (p.Stats.Mental?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Stats.Mental.To)
                        .GreaterThanOrEqualTo(p => p.Stats.Mental.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(PlayerStatsBySkillRangeLimitFilterDto.To), nameof(PlayerStatsBySkillRangeLimitFilterDto.From)));

                    RuleFor(p => p.Stats.Mental.From)
                        .LessThanOrEqualTo(p => p.Stats.Mental.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(PlayerStatsBySkillRangeLimitFilterDto.From), nameof(PlayerStatsBySkillRangeLimitFilterDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });

            When(p => (p.Stats.Skill?.From.HasValue ?? false)
                && (p.Stats.Skill?.To.HasValue ?? false), () =>
                {
#pragma warning disable CS8629 // Nullable value type may be null.
                    RuleFor(p => p.Stats.Skill.To)
                        .GreaterThanOrEqualTo(p => p.Stats.Skill.From.Value)
                        .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(PlayerStatsBySkillRangeLimitFilterDto.To), nameof(PlayerStatsBySkillRangeLimitFilterDto.From)));

                    RuleFor(p => p.Stats.Skill.From)
                        .LessThanOrEqualTo(p => p.Stats.Skill.To.Value)
                        .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(PlayerStatsBySkillRangeLimitFilterDto.From), nameof(PlayerStatsBySkillRangeLimitFilterDto.To)));
#pragma warning restore CS8629 // Nullable value type may be null.
                });
        });
    }
}