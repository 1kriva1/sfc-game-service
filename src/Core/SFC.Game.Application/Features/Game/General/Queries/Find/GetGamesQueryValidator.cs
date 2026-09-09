using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Common.Dto.Common;
using SFC.Game.Application.Features.Common.Validators.Common;
using SFC.Game.Application.Features.Game.General.Common.Dto;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.General.Queries.Find;

public class GetGamesQueryValidator : AbstractValidator<GetGamesQuery>
{
    public GetGamesQueryValidator()
    {
        // pagination request validation
        RuleFor(command => command)
            .SetValidator(new PaginationRequestValidator<GetGamesViewModel, GetGamesFilterDto>());

        // statuses
        When(p => p.Filter?.Statuses?.Any() ?? false, () =>
        {
            RuleForEach(p => p.Filter!.Statuses)
                .Must(status => Enum.IsDefined(typeof(GameStatusEnum), status))
                .WithName(nameof(GetGamesFilterDto.Statuses));
        });

        // general
        SetRulesForGeneralProfile();

        // financial
        SetRulesForFinancialProfile();

        // inventary
        SetRulesForInventaryProfile();
    }

    private void SetRulesForGeneralProfile()
    {
        When(p => p.Filter?.Profile?.General != null, () =>
        {
            RuleFor(p => p.Filter!.Profile!.General!.Name)
                    .MaximumLength(ValidationConstants.NameValueMaxLength)
                    .WithName(nameof(GetGamesGeneralProfileFilterDto.Name));
        });

        When(p => p.Filter?.Profile?.General?.Tags?.Any() ?? false, () =>
        {
            RuleFor(p => p.Filter.Profile!.General!.Tags)
                .Must(tags => tags!.Distinct().Count() == tags!.Count())
                .WithMessage(Localization.MustBeUnique)
                .Must(tags => tags!.Count() <= ValidationConstants.TagsMaxLength)
                .WithName(nameof(GameGeneralProfileDto.Tags))
                .WithMessage(Localization.TagsSizeInvalid.BuildValidationMessage(nameof(GameGeneralProfileDto.Tags), ValidationConstants.TagsMaxLength));

            RuleForEach(p => p.Filter.Profile!.General!.Tags)
                .NotEmpty()
                .WithName(nameof(GameGeneralProfileDto.Tags))
                .WithMessage(Localization.TagEmpty)
                .MaximumLength(ValidationConstants.TagValueMaxLength)
                .WithMessage(Localization.TagMaxLength);
        });

        When(p => p.Filter?.Profile?.General?.Availability?.Date.HasValue ?? false, () =>
        {
            RuleFor(p => p.Filter.Profile!.General!.Availability!.Date)
                .GreaterThan(p => DateOnly.MinValue)
                .WithMessage(p => Localization.MustBeGreaterThan.BuildValidationMessage(nameof(p.Filter.Profile.General.Availability.Date), DateOnly.MinValue))
                .LessThan(p => DateOnly.MaxValue)
                .WithMessage(p => Localization.MustBeLessThan.BuildValidationMessage(nameof(p.Filter.Profile.General.Availability.Date), DateOnly.MaxValue));
        });

        When(p => (p.Filter?.Profile?.General?.Availability?.From.HasValue ?? false)
            && (p.Filter?.Profile?.General.Availability?.To.HasValue ?? false), () =>
        {
            RuleFor(p => p.Filter.Profile!.General!.Availability!.To)
                .GreaterThanOrEqualTo(p => p.Filter.Profile!.General!.Availability!.From!.Value)
                .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(GetGamesAvailabilityLimitDto.To), nameof(GetGamesAvailabilityLimitDto.From)));

            RuleFor(p => p.Filter.Profile!.General!.Availability!.From)
                .LessThanOrEqualTo(p => p.Filter.Profile!.General!.Availability!.To!.Value)
                .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(GetGamesAvailabilityLimitDto.From), nameof(GetGamesAvailabilityLimitDto.To)));
        });
    }

    private void SetRulesForFinancialProfile()
    {
        When(p => (p.Filter?.Profile?.Financial?.PayAmount?.From.HasValue ?? false)
            && (p.Filter?.Profile?.Financial.PayAmount?.To.HasValue ?? false), () =>
            {
                RuleFor(p => p.Filter.Profile!.Financial!.PayAmount!.To)
                    .GreaterThanOrEqualTo(p => p.Filter.Profile!.Financial!.PayAmount!.From!.Value)
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<decimal>.To), nameof(RangeLimitDto<decimal>.From)));

                RuleFor(p => p.Filter.Profile!.Financial!.PayAmount!.From)
                    .LessThanOrEqualTo(p => p.Filter.Profile!.Financial!.PayAmount!.To!.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<decimal>.From), nameof(RangeLimitDto<decimal>.To)));
            });
    }

    private void SetRulesForInventaryProfile()
    {
        When(p => (p.Filter?.Profile?.Inventary?.ShirtsCount?.From.HasValue ?? false)
            && (p.Filter?.Profile?.Inventary.ShirtsCount?.To.HasValue ?? false), () =>
            {
                RuleFor(p => p.Filter.Profile!.Inventary!.ShirtsCount!.To)
                    .GreaterThanOrEqualTo(p => p.Filter.Profile!.Inventary!.ShirtsCount!.From!.Value)
                    .WithMessage(Localization.MustBeGreaterThan.BuildValidationMessage(nameof(RangeLimitDto<int>.To), nameof(RangeLimitDto<int>.From)));

                RuleFor(p => p.Filter.Profile!.Inventary!.ShirtsCount!.From)
                    .LessThanOrEqualTo(p => p.Filter.Profile!.Inventary!.ShirtsCount!.To!.Value)
                    .WithMessage(Localization.MustBeLessThan.BuildValidationMessage(nameof(RangeLimitDto<int>.From), nameof(RangeLimitDto<int>.To)));
            });
    }
}