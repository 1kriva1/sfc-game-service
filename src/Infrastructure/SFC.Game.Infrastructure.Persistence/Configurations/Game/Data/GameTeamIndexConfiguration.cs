using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Data;
public class GameTeamIndexConfiguration : EnumDataEntityConfiguration<GameTeamIndex, GameTeamIndexEnum>
{
    public override void Configure(EntityTypeBuilder<GameTeamIndex> builder)
    {
        builder.ToTable("TeamIndexes", DatabaseConstants.DefaultSchemaName);
        base.Configure(builder);
    }
}