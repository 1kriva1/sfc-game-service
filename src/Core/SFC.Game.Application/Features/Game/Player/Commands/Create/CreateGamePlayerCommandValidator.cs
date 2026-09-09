using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Application.Interfaces.Persistence.Repository.Player;

namespace SFC.Game.Application.Features.Game.Player.Commands.Create;

public class CreateGamePlayerCommandValidator : AbstractValidator<CreateGamePlayerCommand>
{
    public CreateGamePlayerCommandValidator(IGameRepository gameRepository, IPlayerRepository playerRepository, IGamePlayerRepository gamePlayerRepository)
    {
        When(p => p.GamePlayer.StatusId.HasValue, () =>
        {
            RuleFor(p => p.GamePlayer.StatusId)
                .Must(status => Enum.IsDefined(typeof(GamePlayerStatusEnum), status!))
                .WithName(nameof(CreateGamePlayerDto.StatusId));
        });

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await gameRepository.AnyAsync(request.GamePlayer.GameId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.GameNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await playerRepository.AnyAsync(request.GamePlayer.PlayerId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.PlayerNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => !await gamePlayerRepository.AnyAsync(request.GamePlayer.GameId, request.GamePlayer.PlayerId).ConfigureAwait(false))
            .WithException(new ConflictException(Localization.PlayerAlreadyInGame));
    }
}