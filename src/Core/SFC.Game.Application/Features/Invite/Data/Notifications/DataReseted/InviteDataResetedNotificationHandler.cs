using MediatR;

using SFC.Game.Application.Interfaces.Metadata;
using SFC.Game.Domain.Events.Invite;

namespace SFC.Game.Application.Features.Invite.Data.Notifications.DataReseted;
public class InviteDataResetedNotificationHandler(IMetadataService metadataService)
    : INotificationHandler<InviteDataResetedEvent>
{
    private readonly IMetadataService _metadataService = metadataService;

    public async Task Handle(InviteDataResetedEvent notification, CancellationToken cancellationToken)
    {
        await _metadataService.CompleteAsync(MetadataServiceEnum.Invite, MetadataDomainEnum.Data, MetadataTypeEnum.Initialization).ConfigureAwait(false);
    }
}