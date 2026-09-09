using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Data;
public class GamePlayerStatusConfiguration : EnumDataEntityConfiguration<GamePlayerStatus, GamePlayerStatusEnum>
{
    public override void Configure(EntityTypeBuilder<GamePlayerStatus> builder)
    {
        builder.ToTable("PlayerStatuses", DatabaseConstants.DefaultSchemaName);
        base.Configure(builder);
    }
}