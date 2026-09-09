using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Team.Data;
public class TeamPlayerStatusConfiguration : EnumDataEntityConfiguration<TeamPlayerStatus, TeamPlayerStatusEnum>
{
    public override void Configure(EntityTypeBuilder<TeamPlayerStatus> builder)
    {
        builder.ToTable("PlayerStatuses", DatabaseConstants.TeamSchemaName);
        base.Configure(builder);
    }
}