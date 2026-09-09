using FluentValidation;

using SFC.Game.Application.Features.Game.Team.Player.Commands.Create;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Update;
public class UpdateGameTeamPlayerCommandValidator : AbstractValidator<UpdateGameTeamPlayerCommand>
{
    public UpdateGameTeamPlayerCommandValidator()
    {
        RuleFor(p => p.GameTeamPlayer.StatusId)
            .Must(status => Enum.IsDefined(typeof(TeamPlayerStatusEnum), status))
            .WithName(nameof(CreateGameTeamPlayerDto.StatusId));
    }
}