using AutoMapper;

using Grpc.Core;

using MediatR;

using Microsoft.AspNetCore.Authorization;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Application.Features.Game.Team.General.Queries.Find;
using SFC.Game.Application.Features.Game.Team.General.Queries.Get;
using SFC.Game.Contracts.Headers;
using SFC.Game.Contracts.Messages.Game.Team.General.Find;
using SFC.Game.Contracts.Messages.Game.Team.General.Get;
using SFC.Game.Infrastructure.Constants;

using static SFC.Game.Contracts.Services.GameTeamService;

namespace SFC.Game.Api.Services;

[Authorize(Policy.General)]
public class GameTeamService(IMapper mapper, ISender mediator) : GameTeamServiceBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ISender _mediator = mediator;

    public override async Task<GetGameTeamResponse> GetGameTeam(GetGameTeamRequest request, ServerCallContext context)
    {
        GetGameTeamQuery query = _mapper.Map<GetGameTeamQuery>(request);

        GetGameTeamViewModel model = await _mediator.Send(query).ConfigureAwait(true);

        context.AddAuditableHeaderIfRequested(_mapper.Map<AuditableHeader>(model.GameTeam));

        return _mapper.Map<GetGameTeamResponse>(model);
    }

    public override async Task<GetGameTeamsResponse> GetGameTeams(GetGameTeamsRequest request, ServerCallContext context)
    {
        GetGameTeamsQuery query = _mapper.Map<GetGameTeamsQuery>(request);

        GetGameTeamsViewModel result = await _mediator.Send(query).ConfigureAwait(true);

        context.AddPaginationHeader(_mapper.Map<PaginationHeader>(result.Metadata));

        return _mapper.Map<GetGameTeamsResponse>(result);
    }
}