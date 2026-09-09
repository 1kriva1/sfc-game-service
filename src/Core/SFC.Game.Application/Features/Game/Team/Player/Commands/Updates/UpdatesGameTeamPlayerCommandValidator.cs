using FluentValidation;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;

public class UpdatesGameTeamPlayerCommandValidator : AbstractValidator<UpdatesGameTeamPlayerCommand>
{
    public UpdatesGameTeamPlayerCommandValidator()
    {
        RuleForEach(value => value.GameTeamPlayers)
            .ChildRules(gameTeam =>
            {
                gameTeam.RuleFor(p => p.StatusId)
                    .Must(status => Enum.IsDefined(typeof(TeamPlayerStatusEnum), status))
                    .WithName(nameof(UpdatesGameTeamPlayerDto.StatusId));
            });
    }
}