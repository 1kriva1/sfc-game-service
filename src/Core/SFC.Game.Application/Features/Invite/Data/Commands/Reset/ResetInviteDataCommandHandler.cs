using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Invite.Data;
using SFC.Game.Domain.Entities.Invite.Data;
using SFC.Game.Domain.Events.Invite;

namespace SFC.Game.Application.Features.Invite.Data.Commands.Reset;

public class ResetInviteDataCommandHandler(
    IMapper mapper,
    IMediator mediator,
    IInviteStatusRepository inviteStatusRepository) : IRequestHandler<ResetInviteDataCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IInviteStatusRepository _inviteStatusRepository = inviteStatusRepository;

    public async Task Handle(ResetInviteDataCommand request, CancellationToken cancellationToken)
    {
        await _inviteStatusRepository
            .ResetAsync(_mapper.Map<IEnumerable<InviteStatus>>(request.InviteStatuses))
            .ConfigureAwait(false);

        await PublishDataResetedEventAsync(cancellationToken).ConfigureAwait(false);
    }

    private Task PublishDataResetedEventAsync(CancellationToken cancellationToken)
    {
        InviteDataResetedEvent @event = new();
        return _mediator.Publish(@event, cancellationToken);
    }
}