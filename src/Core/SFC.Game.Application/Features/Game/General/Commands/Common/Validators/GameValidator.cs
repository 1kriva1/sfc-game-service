using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Application.Features.Game.General.Commands.Common.Validators;

public class GameValidator<T> : AbstractValidator<T> where T : BaseGameDto
{
    public GameValidator()
    {
        // Keep this if Profile must be provided for create/update.
        RuleFor(p => p.Profile)
            .NotNull()
            .WithMessage(Localization.MustNotBeEmpty);

        When(p => p.Profile is not null, () =>
        {
            SetRulesForGeneralProfile();
            SetRulesForInventaryProfile();
            SetRulesForFinancialProfile();
        });
    }

    private void SetRulesForGeneralProfile()
    {
        RuleFor(p => p.Profile!.General.Name)
           .RequiredProperty(ValidationConstants.NameValueMaxLength, nameof(GameGeneralProfileDto.Name));

        RuleFor(p => p.Profile!.General.Description)
           .MaximumLength(ValidationConstants.DescriptionValueMaxLength)
           .WithName(nameof(GameGeneralProfileDto.Description));

        When(p => p.Profile!.General.Tags.Any(), () =>
        {
            RuleFor(p => p.Profile!.General.Tags)
                .Must(tags => tags.Distinct().Count() == tags.Count())
                .WithMessage(Localization.MustBeUnique)
                .Must(tags => tags.Count() <= ValidationConstants.TagsMaxLength)
                .WithName(nameof(GameGeneralProfileDto.Tags))
                .WithMessage(Localization.TagsSizeInvalid.BuildValidationMessage(nameof(GameGeneralProfileDto.Tags), ValidationConstants.TagsMaxLength));

            RuleForEach(p => p.Profile!.General.Tags)
                .NotEmpty()
                .WithName(nameof(GameGeneralProfileDto.Tags))
                .WithMessage(Localization.TagEmpty)
                .MaximumLength(ValidationConstants.TagValueMaxLength)
                .WithMessage(Localization.TagMaxLength);
        });

        RuleFor(p => p.Profile!.General.Availability.Date)
            .GreaterThan(p => DateOnly.MinValue)
            .WithMessage(p => Localization.MustBeGreaterThan.BuildValidationMessage(nameof(p.Profile.General.Availability.Date), DateOnly.MinValue))
            .LessThan(p => DateOnly.MaxValue)
            .WithMessage(p => Localization.MustBeLessThan.BuildValidationMessage(nameof(p.Profile.General.Availability.Date), DateOnly.MaxValue));

        RuleFor(p => p.Profile!.General.Availability.To)
            .GreaterThan(p => p.Profile!.General.Availability.From)
            .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(GameAvailabilityDto.To), nameof(GameAvailabilityDto.From)));

        RuleFor(p => p.Profile!.General.Availability.From)
            .LessThan(p => p.Profile!.General.Availability.To)
            .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(GameAvailabilityDto.From), nameof(GameAvailabilityDto.To)));
    }

    private void SetRulesForInventaryProfile()
    {
        When(p => p.Profile!.Inventary.ShirtsRequired, () =>
        {
            RuleFor(p => p.Profile!.Inventary.ShirtsCount)
                .NotEmpty()
                .WithName(nameof(GameInventaryProfileDto.ShirtsCount))
                .WithMessage(Localization.MustNotBeEmpty);
        });
    }

    private void SetRulesForFinancialProfile()
    {
        When(p => !p.Profile!.Financial.FreeGame, () =>
        {
            RuleFor(p => p.Profile!.Financial.PayAmount)
                .NotEmpty()
                .WithName(nameof(GameFinancialProfileDto.PayAmount))
                .WithMessage(Localization.MustNotBeEmpty);
        });
    }
}