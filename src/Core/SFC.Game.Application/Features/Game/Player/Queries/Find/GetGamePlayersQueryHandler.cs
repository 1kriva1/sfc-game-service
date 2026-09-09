using AutoMapper;

using MediatR;

using SFC.Game.Application.Features.Common.Dto.Pagination;
using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.Player.Common.Dto;
using SFC.Game.Application.Features.Game.Player.Queries.Find.Extensions;
using SFC.Game.Application.Interfaces.Common;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;
using SFC.Game.Domain.Entities.Game.Player;

namespace SFC.Game.Application.Features.Game.Player.Queries.Find;
public class GetGamePlayersQueryHandler(
    IMapper mapper,
    IGamePlayerRepository gamePlayerRepository,
    IDateTimeService dateTimeService)
    : IRequestHandler<GetGamePlayersQuery, GetGamePlayersViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;
    private readonly IDateTimeService _dateTimeService = dateTimeService;

    public async Task<GetGamePlayersViewModel> Handle(GetGamePlayersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<GamePlayer>> filters = request.Filter.BuildSearchFilters(_dateTimeService.DateNow);

        IEnumerable<Sorting<GamePlayer, dynamic>> sorting = request.Sorting.BuildGamePlayerSorting();

        FindParameters<GamePlayer> parameters = new()
        {
            Pagination = _mapper.Map<Pagination>(request.Pagination),
            Filters = new Filters<GamePlayer>(filters),
            Sorting = new Sortings<GamePlayer>(sorting)
        };

        PagedList<GamePlayer> pageList = await _gamePlayerRepository.FindAsync(parameters, request.Includes)
                                                                    .ConfigureAwait(true);

        return new GetGamePlayersViewModel
        {
            Items = _mapper.Map<IEnumerable<GamePlayerDto>>(pageList),
            Metadata = _mapper.Map<PageMetadataDto>(pageList)
        };
    }
}