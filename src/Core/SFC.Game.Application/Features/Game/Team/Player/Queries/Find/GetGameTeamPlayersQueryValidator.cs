using FluentValidation;

using SFC.Game.Application.Features.Common.Validators.Common;
using SFC.Game.Application.Features.Common.Validators.Player;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Find;
public class GetGameTeamPlayersQueryValidator : AbstractValidator<GetGameTeamPlayersQuery>
{
    public GetGameTeamPlayersQueryValidator()
    {
        // pagination request filter
        RuleFor(command => command)
            .SetValidator(new PaginationRequestValidator<GetGameTeamPlayersViewModel, GetGameTeamPlayersFilterDto>());

        // player filter
        When(p => p?.Filter?.Player != null, () =>
        {
            RuleFor(command => command.Filter.Player!)
                .SetValidator(new PlayerFilterValidator());
        });
    }
}