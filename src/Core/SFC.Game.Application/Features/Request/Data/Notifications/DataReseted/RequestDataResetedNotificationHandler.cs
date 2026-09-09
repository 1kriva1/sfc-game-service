using MediatR;

using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Domain.Events.Request;

namespace SFC.Game.Application.Features.Request.Data.Notifications.DataReseted;
public class RequestDataResetedNotificationHandler(IMetadataService metadataService)
    : INotificationHandler<RequestDataResetedEvent>
{
    private readonly IMetadataService _metadataService = metadataService;

    public async Task Handle(RequestDataResetedEvent notification, CancellationToken cancellationToken)
    {
        await _metadataService.CompleteAsync(MetadataServiceEnum.Request, MetadataDomainEnum.Data, MetadataTypeEnum.Initialization).ConfigureAwait(false);
    }
}