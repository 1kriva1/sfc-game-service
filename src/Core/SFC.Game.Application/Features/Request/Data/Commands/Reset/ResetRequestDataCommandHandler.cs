using AutoMapper;

using MediatR;

using SFC.Game.Application.Interfaces.Persistence.Repository.Request.Data;
using SFC.Game.Domain.Entities.Request.Data;
using SFC.Game.Domain.Events.Request;

namespace SFC.Game.Application.Features.Request.Data.Commands.Reset;

public class ResetRequestDataCommandHandler(
    IMapper mapper,
    IMediator mediator,
    IRequestStatusRepository RequestStatusRepository) : IRequestHandler<ResetRequestDataCommand>
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IRequestStatusRepository _RequestStatusRepository = RequestStatusRepository;

    public async Task Handle(ResetRequestDataCommand request, CancellationToken cancellationToken)
    {
        await _RequestStatusRepository
            .ResetAsync(_mapper.Map<IEnumerable<RequestStatus>>(request.RequestStatuses))
            .ConfigureAwait(false);

        await PublishDataResetedEventAsync(cancellationToken).ConfigureAwait(false);
    }

    private Task PublishDataResetedEventAsync(CancellationToken cancellationToken)
    {
        RequestDataResetedEvent @event = new();
        return _mediator.Publish(@event, cancellationToken);
    }
}