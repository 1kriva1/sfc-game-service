using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Data;
public class GameStatusConfiguration : EnumDataEntityConfiguration<GameStatus, GameStatusEnum>
{
    public override void Configure(EntityTypeBuilder<GameStatus> builder)
    {
        builder.ToTable("Statuses", DatabaseConstants.DefaultSchemaName);
        base.Configure(builder);
    }
}