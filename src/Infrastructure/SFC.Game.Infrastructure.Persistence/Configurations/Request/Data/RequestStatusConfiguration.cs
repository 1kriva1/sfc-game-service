using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Request.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Request.Data;
public class RequestStatusConfiguration : EnumDataEntityConfiguration<RequestStatus, RequestStatusEnum>
{
    public override void Configure(EntityTypeBuilder<RequestStatus> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("RequestStatuses", DatabaseConstants.RequestSchemaName);
        base.Configure(builder);
    }
}