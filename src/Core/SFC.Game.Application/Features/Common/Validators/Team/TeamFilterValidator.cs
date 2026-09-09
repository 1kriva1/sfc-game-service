using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Common.Dto.Team.General.Filters;
using SFC.Game.Application.Common.Extensions;

namespace SFC.Game.Application.Features.Common.Validators.Team;

public class TeamFilterValidator : AbstractValidator<TeamFilterDto>
{
    public TeamFilterValidator()
    {
        SetRulesForGeneralProfile();
    }

    private void SetRulesForGeneralProfile()
    {
        When(p => p.Profile?.General != null, () =>
        {
            RuleFor(p => p.Profile!.General!.Name)
                    .MaximumLength(ValidationConstants.NameValueMaxLength)
                    .WithName(nameof(TeamGeneralProfileFilterDto.Name));

            RuleFor(p => p.Profile!.General!.City)
                    .MaximumLength(ValidationConstants.CityValueMaxLength)
                    .WithName(nameof(TeamGeneralProfileFilterDto.City));
        });

        When(p => p.Profile?.General?.Tags?.Any() ?? false, () =>
        {
            RuleFor(p => p.Profile!.General!.Tags)
                .Must(tags => tags!.Distinct().Count() == tags!.Count())
                .WithMessage(Localization.MustBeUnique)
                .Must(tags => tags!.Count() <= ValidationConstants.TagsMaxLength)
                .WithName(nameof(TeamGeneralProfileDto.Tags))
                .WithMessage(Localization.TagsSizeInvalid.BuildValidationMessage(nameof(TeamGeneralProfileDto.Tags), ValidationConstants.TagsMaxLength));

            RuleForEach(p => p.Profile!.General!.Tags)
                .NotEmpty()
                .WithName(nameof(TeamGeneralProfileDto.Tags))
                .WithMessage(Localization.TagEmpty)
                .MaximumLength(ValidationConstants.TagValueMaxLength)
                .WithMessage(Localization.TagMaxLength);
        });

        When(p => p.Profile?.General?.Availability?.Days?.Any() ?? false, () =>
        {
            RuleFor(p => p.Profile!.General!.Availability!.Days)
                .Must(days => days.Count() <= Enum.GetNames(typeof(DayOfWeek)).Length)
                .WithMessage(Localization.MustNotExceedSize.BuildValidationMessage(nameof(TeamAvailabilityLimitDto.Days), Enum.GetNames(typeof(DayOfWeek)).Length));

            RuleForEach(p => p.Profile!.General!.Availability!.Days)
                .IsInEnum()
                .WithName(nameof(TeamAvailabilityLimitDto.Days))
                .WithMessage(Localization.AvailabilityDayInvalid);

        });

        When(p => (p.Profile?.General?.Availability?.From.HasValue ?? false)
            && (p.Profile?.General.Availability?.To.HasValue ?? false), () =>
            {
                RuleFor(p => p.Profile!.General!.Availability!.To)
                    .GreaterThanOrEqualTo(p => p.Profile!.General!.Availability!.From!.Value)
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(TeamAvailabilityLimitDto.To), nameof(TeamAvailabilityLimitDto.From)));

                RuleFor(p => p.Profile!.General!.Availability!.From)
                    .LessThanOrEqualTo(p => p.Profile!.General!.Availability!.To!.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(TeamAvailabilityLimitDto.From), nameof(TeamAvailabilityLimitDto.To)));
            });
    }
}