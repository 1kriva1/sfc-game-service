using AutoMapper;

using MediatR;

using SFC.Game.Application.Features.Common.Dto.Pagination;
using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.Team.Player.Common.Dto;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Extensions;
using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Queries.Find;
public class GetGameTeamPlayersQueryHandler(
    IMapper mapper,
    IGameTeamPlayerRepository teamPlayerRepository,
    IDateTimeService dateTimeService)
    : IRequestHandler<GetGameTeamPlayersQuery, GetGameTeamPlayersViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameTeamPlayerRepository _teamPlayerRepository = teamPlayerRepository;
    private readonly IDateTimeService _dateTimeService = dateTimeService;

    public async Task<GetGameTeamPlayersViewModel> Handle(GetGameTeamPlayersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<GameTeamPlayer>> filters = request.Filter.BuildSearchFilters(_dateTimeService.DateNow);

        IEnumerable<Sorting<GameTeamPlayer, dynamic>> sorting = request.Sorting.BuildGameTeamPlayerSorting();

        FindParameters<GameTeamPlayer> parameters = new()
        {
            Pagination = _mapper.Map<Pagination>(request.Pagination),
            Filters = new Filters<GameTeamPlayer>(filters),
            Sorting = new Sortings<GameTeamPlayer>(sorting)
        };

        PagedList<GameTeamPlayer> pageList = await _teamPlayerRepository.FindAsync(parameters, request.Includes)
                                                                        .ConfigureAwait(true);

        return new GetGameTeamPlayersViewModel
        {
            Items = _mapper.Map<IEnumerable<GameTeamPlayerDto>>(pageList),
            Metadata = _mapper.Map<PageMetadataDto>(pageList)
        };
    }
}