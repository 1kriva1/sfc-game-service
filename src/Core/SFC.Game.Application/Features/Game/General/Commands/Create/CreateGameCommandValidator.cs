using FluentValidation;

using SFC.Game.Application.Features.Game.General.Commands.Common.Validators;

namespace SFC.Game.Application.Features.Game.General.Commands.Create;
public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(command => command.Game).SetValidator(new GameValidator<CreateGameDto>());
    }
}