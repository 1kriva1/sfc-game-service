using AutoMapper;

using MediatR;

using SFC.Game.Application.Features.Common.Dto.Pagination;
using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Extensions;
using SFC.Game.Application.Features.Game.Team.General.Common.Dto;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find.Extensions;
using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.General;

namespace SFC.Game.Application.Features.Game.Team.General.Queries.Find;
public class GetGameTeamsQueryHandler(
    IMapper mapper,
    IGameTeamRepository gameTeamRepository,
    IDateTimeService dateTimeService)
    : IRequestHandler<GetGameTeamsQuery, GetGameTeamsViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamRepository _gameTeamRepository = gameTeamRepository;
    private readonly IDateTimeService _dateTimeService = dateTimeService;

    public async Task<GetGameTeamsViewModel> Handle(GetGameTeamsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<GameTeam>> filters = request.Filter.BuildSearchFilters(_dateTimeService.DateNow);

        IEnumerable<Sorting<GameTeam, dynamic>> sorting = request.Sorting.BuildGameTeamSorting();

        FindParameters<GameTeam> parameters = new()
        {
            Pagination = _mapper.Map<Pagination>(request.Pagination),
            Filters = new Filters<GameTeam>(filters),
            Sorting = new Sortings<GameTeam>(sorting)
        };

        PagedList<GameTeam> pageList = await _gameTeamRepository.FindAsync(parameters, request.Includes)
                                                                .ConfigureAwait(true);

        return new GetGameTeamsViewModel
        {
            Items = _mapper.Map<IEnumerable<GameTeamDto>>(pageList),
            Metadata = _mapper.Map<PageMetadataDto>(pageList)
        };
    }
}