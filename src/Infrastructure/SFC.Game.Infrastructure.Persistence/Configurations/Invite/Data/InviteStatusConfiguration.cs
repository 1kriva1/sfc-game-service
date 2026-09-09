using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Invite.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Invite.Data;
public class InviteStatusConfiguration : EnumDataEntityConfiguration<InviteStatus, InviteStatusEnum>
{
    public override void Configure(EntityTypeBuilder<InviteStatus> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("InviteStatuses", DatabaseConstants.InviteSchemaName);
        base.Configure(builder);
    }
}