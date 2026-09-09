using AutoMapper;

using MediatR;

using SFC.Game.Application.Features.Common.Dto.Pagination;
using SFC.Game.Application.Features.Common.Models.Find;
using SFC.Game.Application.Features.Common.Models.Find.Filters;
using SFC.Game.Application.Features.Common.Models.Find.Paging;
using SFC.Game.Application.Features.Common.Models.Find.Sorting;
using SFC.Game.Application.Features.Game.General.Common.Dto;
using SFC.Game.Application.Features.Game.General.Queries.Find.Extensions;
using SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;

namespace SFC.Game.Application.Features.Game.General.Queries.Find;
public class GetGamesQueryHandler(
    IMapper mapper,
    IGameRepository gameRepository)
    : IRequestHandler<GetGamesQuery, GetGamesViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameRepository _gameRepository = gameRepository;

    public async Task<GetGamesViewModel> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Filter<GameEntity>> filters = request.Filter.BuildSearchFilters();

        IEnumerable<Sorting<GameEntity, dynamic>>? sorting = request.Sorting.BuildGameSearchSorting();

        FindParameters<GameEntity> parameters = new()
        {
            Pagination = _mapper.Map<Pagination>(request.Pagination),
            Filters = new Filters<GameEntity>(filters),
            Sorting = new Sortings<GameEntity>(sorting)
        };

        PagedList<GameEntity> pageList = await _gameRepository.FindAsync(parameters, request.Includes)
                                                              .ConfigureAwait(true);

        return new GetGamesViewModel
        {
            Items = _mapper.Map<IEnumerable<GameDto>>(pageList),
            Metadata = _mapper.Map<PageMetadataDto>(pageList)
        };
    }
}