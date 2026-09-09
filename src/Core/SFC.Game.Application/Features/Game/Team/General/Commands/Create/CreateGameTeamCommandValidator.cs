using FluentValidation;

using SFC.Game.Application.Common.Constants;
using SFC.Game.Application.Common.Exceptions;
using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Domain.Enums.Game;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Create;

public class CreateGameTeamCommandValidator : AbstractValidator<CreateGameTeamCommand>
{
    private readonly IGameRepository _gameRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IGameTeamRepository _gameTeamRepository;

    public CreateGameTeamCommandValidator(IGameRepository gameRepository, ITeamRepository teamRepository, IGameTeamRepository gameTeamRepository)
    {
        _teamRepository = teamRepository;
        _gameRepository = gameRepository;
        _gameTeamRepository = gameTeamRepository;

        When(p => p.GameTeam.StatusId.HasValue, () =>
        {
            RuleFor(p => p.GameTeam.StatusId)
                .Must(status => Enum.IsDefined(typeof(GameTeamStatusEnum), status!))
                .WithName(nameof(CreateGameTeamDto.StatusId));
        });

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

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await _gameRepository.AnyAsync(request.GameTeam.GameId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.GameNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => await _teamRepository.AnyAsync(request.GameTeam.TeamId).ConfigureAwait(true))
            .WithException(new NotFoundException(Localization.TeamNotFound));

        RuleFor(value => value)
            .MustAsync(async (request, cancellation) => !await _gameTeamRepository.AnyAsync(request.GameTeam.GameId, request.GameTeam.TeamId).ConfigureAwait(false))
            .WithException(new ConflictException(Localization.TeamAlreadyInGame));
    }
}