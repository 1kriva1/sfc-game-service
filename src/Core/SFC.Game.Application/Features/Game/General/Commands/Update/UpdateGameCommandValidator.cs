using FluentValidation;

using SFC.Game.Application.Features.Game.General.Commands.Common.Validators;

namespace SFC.Game.Application.Features.Game.General.Commands.Update;
public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(command => command.Game).SetValidator(new GameValidator<UpdateGameDto>());
    }
}