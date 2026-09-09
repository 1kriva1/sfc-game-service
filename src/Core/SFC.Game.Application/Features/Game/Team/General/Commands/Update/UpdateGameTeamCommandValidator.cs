using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Update;
public class UpdateGameTeamCommandValidator : AbstractValidator<UpdateGameTeamCommand>
{
    public UpdateGameTeamCommandValidator()
    {
        RuleFor(p => p.GameTeam.StatusId)
                .Must(status => Enum.IsDefined(typeof(GameTeamStatusEnum), status!))
                .WithName(nameof(CreateGameTeamDto.StatusId));

        When(p => p.GameTeam.Index.HasValue, () =>
        {
            RuleFor(p => p.GameTeam.Index)
                .Must(index => Enum.IsDefined(typeof(GameTeamIndexEnum), index!))
                .WithName(nameof(CreateGameTeamDto.Index));
        });

        When(p => p.GameTeam.StatusId == (int)GameTeamStatusEnum.InGame, () =>
        {
            RuleFor(p => p.GameTeam.Index)
                .NotEmpty()
                .WithName(nameof(CreateGameTeamDto.Index))
                .WithMessage(Localization.MustNotBeEmpty);
        });
    }
}