using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Data;
public class WorkingFootConfiguration : EnumDataEntityConfiguration<WorkingFoot, WorkingFootEnum>
{
    public override void Configure(EntityTypeBuilder<WorkingFoot> builder)
    {
        builder.ToTable("WorkingFoots", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}