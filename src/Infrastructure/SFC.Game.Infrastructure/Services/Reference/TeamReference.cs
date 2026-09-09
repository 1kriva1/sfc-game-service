using AutoMapper;

using SFC.Game.Application.Common.Dto.Team.General;
using SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;
using SFC.Game.Application.Interfaces.Reference;
using SFC.Game.Application.Interfaces.Team.General;

namespace SFC.Game.Infrastructure.Services.Reference;
public class TeamReference(
    IMapper mapper,
    ITeamRepository teamRepository,
    ITeamService teamService) : BaseReference<TeamEntity, long, TeamDto>(mapper), ITeamReference
{
    private readonly ITeamRepository _teamRepository = teamRepository;
    private readonly ITeamService _teamService = teamService;

    protected override Task<TeamEntity?> GetFromLocalAsync(long id, CancellationToken cancellationToken = default)
        => _teamRepository.GetByIdAsync(id);

    protected override Task<TeamDto?> GetFromReferenceAsync(long id, CancellationToken cancellationToken = default)
        => _teamService.GetTeamAsync(id, cancellationToken);

    protected override Task<TeamEntity> AddLocalAsync(TeamEntity entity, CancellationToken cancellationToken = default)
        => _teamRepository.AddAsync(entity);
}