using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Data;
public class StatTypeConfiguration : EnumDataEntityConfiguration<StatType, StatTypeEnum>
{
    public override void Configure(EntityTypeBuilder<StatType> builder)
    {
        builder.ToTable("StatTypes", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}