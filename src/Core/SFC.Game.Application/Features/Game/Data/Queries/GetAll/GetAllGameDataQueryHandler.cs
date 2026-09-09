using AutoMapper;

using MediatR;

using SFC.Game.Application.Common.Extensions;
using SFC.Game.Application.Features.Game.Data.Queries.Common.Dto;
using SFC.Game.Application.Interfaces.Game.Data;
using SFC.Game.Application.Interfaces.Game.Data.Models;

namespace SFC.Game.Application.Features.Game.Data.Queries.GetAll;
public class GetAllGameDataQueryHandler(IMapper mapper, IGameDataService gameDataService)
    : IRequestHandler<GetAllGameDataQuery, GetAllGameDataViewModel>
{
    private readonly IMapper _mapper = mapper;
    private readonly IGameDataService _gameDataService = gameDataService;

    public async Task<GetAllGameDataViewModel> Handle(GetAllGameDataQuery request, CancellationToken cancellationToken)
    {
        GetAllGameDataModel model = await _gameDataService.GetAllGameDataAsync().ConfigureAwait(true);

        return new GetAllGameDataViewModel
        {
            GameStatuses = _mapper.Map<IEnumerable<DataValueDto>>(model.GameStatuses.Localize()),
            GameTeamStatuses = _mapper.Map<IEnumerable<DataValueDto>>(model.GameTeamStatuses.Localize()),
            GamePlayerStatuses = _mapper.Map<IEnumerable<DataValueDto>>(model.GamePlayerStatuses.Localize()),
            GameTeamIndexes = _mapper.Map<IEnumerable<DataValueDto>>(model.GameTeamIndexes.Localize())
        };
    }
}