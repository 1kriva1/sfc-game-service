using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Data;
public class GameTeamStatusConfiguration : EnumDataEntityConfiguration<GameTeamStatus, GameTeamStatusEnum>
{
    public override void Configure(EntityTypeBuilder<GameTeamStatus> builder)
    {
        builder.ToTable("TeamStatuses", DatabaseConstants.DefaultSchemaName);
        base.Configure(builder);
    }
}