using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Game.Team.Player;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Domain.Entities.Team.Player;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Team.Player;

public class GameTeamPlayerConfiguration : AuditableEntityConfiguration<GameTeamPlayer, long>
{
    public override void Configure(EntityTypeBuilder<GameTeamPlayer> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasIndex(t => new { t.GameTeamId, t.GamePlayerId, t.TeamPlayerId })
               .IsUnique();

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(t => t.UserId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(t => t.GameTeam)
               .WithMany(t => t.Players)
               .HasForeignKey(t => t.GameTeamId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(t => t.GamePlayer)
               .WithOne(gp => gp.GameTeamPlayer)
               .HasForeignKey<GameTeamPlayer>(t => t.GamePlayerId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(t => t.TeamPlayer)
               .WithMany()
               .HasForeignKey(t => t.TeamPlayerId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("TeamPlayers");

        base.Configure(builder);
    }
}