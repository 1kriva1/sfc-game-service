using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Metadata;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Metadata;
public class MetadataStateConfiguration : EnumEntityConfiguration<MetadataState, MetadataStateEnum>
{
    public override void Configure(EntityTypeBuilder<MetadataState> builder)
    {
        builder.ToTable("States", DatabaseConstants.MetadataSchemaName);
        base.Configure(builder);
    }
}