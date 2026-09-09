using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;
using SFC.Game.Infrastructure.Persistence.Constants;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Team.Player;
public class TeamPlayerConfiguration : AuditableReferenceEntityConfiguration<TeamPlayer, long>
{
    public override void Configure(EntityTypeBuilder<TeamPlayer> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        // it's for skip exception during update db (sql server only related)
        builder.HasOne(e => e.Team)
               .WithMany(e => e.Players)
               .HasForeignKey(e => e.TeamId)
               .OnDelete(DeleteBehavior.ClientCascade);

        // it's for skip exception during update db (sql server only related)
        builder.HasOne(e => e.Player)
               .WithMany(e => e.Teams)
               .HasForeignKey(e => e.PlayerId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne<TeamPlayerStatus>()
               .WithMany()
               .HasForeignKey(t => t.StatusId)
               .IsRequired(true);

        builder.HasOne<User>()
               .WithMany()
               .IsRequired(true);

        builder.ToTable("Players", DatabaseConstants.TeamSchemaName);

        base.Configure(builder);
    }
}