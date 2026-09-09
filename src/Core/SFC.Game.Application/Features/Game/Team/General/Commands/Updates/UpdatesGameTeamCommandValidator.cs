using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.Team.General.Commands.Create;
using SFC.Game.Domain.Enums.Game;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Updates;

public class UpdatesGameTeamCommandValidator : AbstractValidator<UpdatesGameTeamCommand>
{
    public UpdatesGameTeamCommandValidator()
    {
        RuleFor(value => value.GameTeams)
                .Must(gameTeams => gameTeams.Count(gameTeam => gameTeam.Index == (int)GameTeamIndex.A) <= 1 &&
                                   gameTeams.Count(gameTeam => gameTeam.Index == (int)GameTeamIndex.B) <= 1)
                .WithName(nameof(UpdatesGameTeamDto.Index))
                .WithException(new NotFoundException(Localization.MustBeUnique))
                .Must(gameTeams => gameTeams.Count(gameTeam => gameTeam.StatusId == (int)GameTeamStatus.InGame) <= 2)
                .WithName(nameof(UpdatesGameTeamDto.StatusId))
                .WithMessage(Localization.GameTeamWithStatusInGameInvalidCount);

        RuleForEach(value => value.GameTeams)
            .ChildRules(gameTeam =>
            {
                gameTeam.RuleFor(p => p.TeamId)
                        .NotEmpty()
                        .WithName(nameof(CreateGameTeamDto.TeamId))
                        .WithMessage(Localization.MustNotBeEmpty);

                gameTeam.RuleFor(p => p.StatusId)
                    .Must(status => Enum.IsDefined(typeof(GameTeamStatusEnum), status))
                    .WithName(nameof(CreateGameTeamDto.StatusId));

                gameTeam.When(p => p.Index.HasValue, () =>
                {
                    gameTeam.RuleFor(p => p.Index)
                        .Must(index => Enum.IsDefined(typeof(GameTeamIndexEnum), index!))
                        .WithName(nameof(CreateGameTeamDto.Index));
                });

                gameTeam.When(p => p.StatusId == (int)GameTeamStatusEnum.InGame, () =>
                {
                    gameTeam.RuleFor(p => p.Index)
                        .NotEmpty()
                        .WithName(nameof(CreateGameTeamDto.Index))
                        .WithMessage(Localization.MustNotBeEmpty);
                });
            });
    }
}