using FluentValidation;

using SFC.Game.Application.Features.Common.Validators.Common;
using SFC.Game.Application.Features.Common.Validators.Team;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Find;
public class GetGameTeamsQueryValidator : AbstractValidator<GetGameTeamsQuery>
{
    public GetGameTeamsQueryValidator()
    {
        // pagination request filter
        RuleFor(command => command)
            .SetValidator(new PaginationRequestValidator<GetGameTeamsViewModel, GetGameTeamsFilterDto>());

        // team filter
        When(p => p?.Filter?.Team != null, () =>
        {
            RuleFor(command => command.Filter.Team!)
                .SetValidator(new TeamFilterValidator());
        });
    }
}