using AutoMapper;

using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Authorization;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Application.Features.Game.Player.Queries.Find;
using SFC.Game.Application.Features.Game.Player.Queries.Get;
using SFC.Game.Contracts.Headers;
using SFC.Game.Contracts.Messages.Game.Player.Find;
using SFC.Game.Contracts.Messages.Game.Player.Get;
using SFC.Game.Infrastructure.Constants;

using static SFC.Game.Contracts.Services.GamePlayerService;

namespace SFC.Game.Api.Services;

[Authorize(Policy.General)]
public class GamePlayerService(IMapper mapper, ISender mediator) : GamePlayerServiceBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public override async Task<GetGamePlayerResponse> GetGamePlayer(GetGamePlayerRequest request, ServerCallContext context)
    {
        GetGamePlayerQuery query = _mapper.Map<GetGamePlayerQuery>(request);

        GetGamePlayerViewModel model = await _mediator.Send(query).ConfigureAwait(true);

        context.AddAuditableHeaderIfRequested(_mapper.Map<AuditableHeader>(model.GamePlayer));

        return _mapper.Map<GetGamePlayerResponse>(model);
    }

    public override async Task<GetGamePlayersResponse> GetGamePlayers(GetGamePlayersRequest request, ServerCallContext context)
    {
        GetGamePlayersQuery query = _mapper.Map<GetGamePlayersQuery>(request);

        GetGamePlayersViewModel result = await _mediator.Send(query).ConfigureAwait(true);

        context.AddPaginationHeader(_mapper.Map<PaginationHeader>(result.Metadata));

        return _mapper.Map<GetGamePlayersResponse>(result);
    }
}