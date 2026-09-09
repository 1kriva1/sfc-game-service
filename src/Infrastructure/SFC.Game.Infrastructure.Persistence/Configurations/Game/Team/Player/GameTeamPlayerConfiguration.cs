using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Domain.Entities.Team.Data;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Team.Player;
public class GameTeamPlayerConfiguration : AuditableEntityConfiguration<GameTeamPlayer, long>
{
    public override void Configure(EntityTypeBuilder<GameTeamPlayer> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasIndex(t => new { t.GameTeamId, t.PlayerId })
               .IsUnique();

        builder.HasOne<TeamPlayerStatus>()
               .WithMany()
               .HasForeignKey(t => t.StatusId)
               .IsRequired(true);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(t => t.UserId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne<GameTeam>(t => t.GameTeam)
               .WithMany(t => t.Players)
               .HasForeignKey(t => t.GameTeamId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.ToTable("TeamPlayers");

        base.Configure(builder);
    }
}