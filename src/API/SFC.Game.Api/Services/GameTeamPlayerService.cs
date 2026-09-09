using AutoMapper;

using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Authorization;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Get;
using SFC.Game.Contracts.Headers;
using SFC.Game.Contracts.Messages.Game.Team.Player.Find;
using SFC.Game.Contracts.Messages.Game.Team.Player.Get;
using SFC.Game.Infrastructure.Constants;

using static SFC.Game.Contracts.Services.GameTeamPlayerService;

namespace SFC.Game.Api.Services;

[Authorize(Policy.General)]
public class GameTeamPlayerService(IMapper mapper, ISender mediator) : GameTeamPlayerServiceBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public override async Task<GetGameTeamPlayerResponse> GetGameTeamPlayer(GetGameTeamPlayerRequest request, ServerCallContext context)
    {
        GetGameTeamPlayerQuery query = _mapper.Map<GetGameTeamPlayerQuery>(request);

        GetGameTeamPlayerViewModel model = await _mediator.Send(query).ConfigureAwait(true);

        context.AddAuditableHeaderIfRequested(_mapper.Map<AuditableHeader>(model.GameTeamPlayer));

        return _mapper.Map<GetGameTeamPlayerResponse>(model);
    }

    public override async Task<GetGameTeamPlayersResponse> GetGameTeamPlayers(GetGameTeamPlayersRequest request, ServerCallContext context)
    {
        GetGameTeamPlayersQuery query = _mapper.Map<GetGameTeamPlayersQuery>(request);

        GetGameTeamPlayersViewModel result = await _mediator.Send(query).ConfigureAwait(true);

        context.AddPaginationHeader(_mapper.Map<PaginationHeader>(result.Metadata));

        return _mapper.Map<GetGameTeamPlayersResponse>(result);
    }
}