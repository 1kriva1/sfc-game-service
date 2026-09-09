using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Create;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;
public class CreatesGameTeamPlayerCommandValidator : AbstractValidator<CreatesGameTeamPlayerCommand>
{
    public CreatesGameTeamPlayerCommandValidator(
        IGameRepository gameRepository,
        ITeamRepository teamRepository,
        IPlayerRepository playerRepository,
        IGameTeamPlayerRepository gameTeamPlayerRepository)
    {
        RuleForEach(value => value.GameTeamPlayers)
            .ChildRules(gameTeamPlayer =>
            {
                gameTeamPlayer.When(p => p.StatusId.HasValue, () =>
                {
                    gameTeamPlayer.RuleFor(p => p.StatusId)
                                .Must(status => Enum.IsDefined(typeof(TeamPlayerStatusEnum), status!))
                                .WithName(nameof(CreateGameTeamPlayerDto.StatusId));
                });

                gameTeamPlayer.RuleFor(value => value)
                    .MustAsync(async (request, cancellation) => await gameRepository.AnyAsync(request.GameId).ConfigureAwait(true))
                    .WithException(new NotFoundException(Localization.GameNotFound));

                gameTeamPlayer.RuleFor(value => value)
                    .MustAsync(async (request, cancellation) => await teamRepository.AnyAsync(request.TeamId).ConfigureAwait(true))
                    .WithException(new NotFoundException(Localization.TeamNotFound));

                gameTeamPlayer.RuleFor(value => value)
                    .MustAsync(async (request, cancellation) => await playerRepository.AnyAsync(request.PlayerId).ConfigureAwait(true))
                    .WithException(new NotFoundException(Localization.PlayerNotFound));

                gameTeamPlayer.RuleFor(value => value)
                    .MustAsync(async (request, cancellation) => !await gameTeamPlayerRepository.AnyAsync(request.GameId, request.TeamId, request.PlayerId).ConfigureAwait(false))
                    .WithException(new ConflictException(Localization.PlayerAlreadyInTeam));
            });
    }
}