using AutoMapper;

using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Authorization;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Application.Features.Game.General.Queries.Find;
using SFC.Game.Application.Features.Game.General.Queries.Get;
using SFC.Game.Contracts.Headers;
using SFC.Game.Contracts.Messages.Game.General.Find;
using SFC.Game.Contracts.Messages.Game.General.Get;
using SFC.Game.Infrastructure.Constants;

using static SFC.Game.Contracts.Services.GameService;

namespace SFC.Game.Api.Services;

[Authorize(Policy.General)]
public class GameService(IMapper mapper, ISender mediator) : GameServiceBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public override async Task<GetGameResponse> GetGame(GetGameRequest request, ServerCallContext context)
    {
        GetGameQuery query = _mapper.Map<GetGameQuery>(request);

        GetGameViewModel model = await _mediator.Send(query).ConfigureAwait(true);

        context.AddAuditableHeaderIfRequested(_mapper.Map<AuditableHeader>(model.Game));

        return _mapper.Map<GetGameResponse>(model);
    }

    public override async Task<GetGamesResponse> GetGames(GetGamesRequest request, ServerCallContext context)
    {
        GetGamesQuery query = _mapper.Map<GetGamesQuery>(request);

        GetGamesViewModel result = await _mediator.Send(query).ConfigureAwait(true);

        context.AddPaginationHeader(_mapper.Map<PaginationHeader>(result.Metadata));

        return _mapper.Map<GetGamesResponse>(result);
    }
}