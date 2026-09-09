using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Create;
public class CreateGameTeamPlayerCommandValidator : AbstractValidator<CreateGameTeamPlayerCommand>
{
    public CreateGameTeamPlayerCommandValidator(
        IGameRepository gameRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
        IGameTeamPlayerRepository gameTeamPlayerRepository)
    {
        When(p => p.GameTeamPlayer.StatusId.HasValue, () =>
        {
            RuleFor(p => p.GameTeamPlayer.StatusId)
                        .Must(status => Enum.IsDefined(typeof(TeamPlayerStatusEnum), status!))
                        .WithName(nameof(CreateGameTeamPlayerDto.StatusId));
        });

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await gameRepository.AnyAsync(request.GameTeamPlayer.GameId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.GameNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await teamRepository.AnyAsync(request.GameTeamPlayer.TeamId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.TeamNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await playerRepository.AnyAsync(request.GameTeamPlayer.PlayerId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.PlayerNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => !await gameTeamPlayerRepository.AnyAsync(request.GameTeamPlayer.GameId, request.GameTeamPlayer.TeamId, request.GameTeamPlayer.PlayerId).ConfigureAwait(false))
            .WithException(new ConflictException(Localization.PlayerAlreadyInTeam));
    }
}