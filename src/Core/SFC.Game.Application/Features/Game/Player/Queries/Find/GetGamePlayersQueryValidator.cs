using FluentValidation;

using SFC.Game.Application.Features.Common.Validators.Common;
using SFC.Game.Application.Features.Common.Validators.Player;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.Player.Queries.Find;
public class GetGamePlayersQueryValidator : AbstractValidator<GetGamePlayersQuery>
{
    public GetGamePlayersQueryValidator()
    {
        // pagination request filter
        RuleFor(command => command)
            .SetValidator(new PaginationRequestValidator<GetGamePlayersViewModel, GetGamePlayersFilterDto>());

        // player filter
        When(p => p?.Filter?.Player != null, () =>
        {
            RuleFor(command => command.Filter.Player!)
                .SetValidator(new PlayerFilterValidator());
        });
    }
}