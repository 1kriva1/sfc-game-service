using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Team.General;
public class GameTeamConfiguration : AuditableEntityConfiguration<GameTeam, long>
{
    public override void Configure(EntityTypeBuilder<GameTeam> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasIndex(t => new { t.GameId, t.TeamId })
               .IsUnique();

        builder.HasOne<GameTeamStatus>()
               .WithMany()
               .HasForeignKey(t => t.StatusId)
               .IsRequired(true);

        builder.HasOne<GameTeamIndex>()
               .WithMany()
               .HasForeignKey(t => t.Index)
               .IsRequired(false);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(t => t.UserId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(e => e.Players)
               .WithOne()
               .HasForeignKey(e => e.GameTeamId)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.ToTable("Teams");

        base.Configure(builder);
    }
}