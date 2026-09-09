using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Data;
public class GameStyleConfiguration : EnumDataEntityConfiguration<GameStyle, GameStyleEnum>
{
    public override void Configure(EntityTypeBuilder<GameStyle> builder)
    {
        builder.ToTable("GameStyles", DatabaseConstants.DataSchemaName);
        base.Configure(builder);
    }
}