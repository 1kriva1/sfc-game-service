using AutoMapper;

using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Authorization;

using SFC.Game.Application.Features.Game.Data.Queries.GetAll;
using SFC.Game.Contracts.Messages.Game.Data.GetAll;
using SFC.Game.Infrastructure.Constants;

using static SFC.Game.Contracts.Services.GameDataService;

namespace SFC.Game.Api.Services;

[Authorize(Policy.General)]
public class GameDataService(IMapper mapper, ISender mediator) : GameDataServiceBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public override async Task<GetAllGameDataResponse> GetAll(GetAllGameDataRequest request, ServerCallContext context)
    {
        GetAllGameDataQuery query = new();

        GetAllGameDataViewModel model = await _mediator.Send(query).ConfigureAwait(true);

        return _mapper.Map<GetAllGameDataResponse>(model);
    }
}