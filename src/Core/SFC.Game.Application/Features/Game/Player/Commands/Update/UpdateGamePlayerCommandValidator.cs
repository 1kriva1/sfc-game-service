using FluentValidation;

namespace SFC.Game.Application.Features.Game.Player.Commands.Update;
public class UpdateGamePlayerCommandValidator : AbstractValidator<UpdateGamePlayerCommand>
{
    public UpdateGamePlayerCommandValidator()
    {
        When(p => p.GamePlayer.StatusId.HasValue, () =>
        {
            RuleFor(p => p.GamePlayer.StatusId)
                .Must(status => Enum.IsDefined(typeof(GamePlayerStatusEnum), status!))
                .WithName(nameof(UpdateGamePlayerDto.StatusId));
        });
    }
}