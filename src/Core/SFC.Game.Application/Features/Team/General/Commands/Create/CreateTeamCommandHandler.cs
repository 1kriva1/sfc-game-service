using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;

namespace SFC.Game.Application.Features.Team.General.Commands.Create;

public class CreateTeamCommandHandler(
    IMapper mapper,
    ITeamRepository teamRepository) : IRequestHandler<CreateTeamCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly ITeamRepository _teamRepository = teamRepository;

    public async Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        TeamEntity team = _mapper.Map<TeamEntity>(request.Team);

        await _teamRepository.AddAsync(team)
                             .ConfigureAwait(false);
    }
}