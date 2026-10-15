using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Game.Domain.Entities.Game.Data;
using SFC.Game.Domain.Entities.Game.Player;
using SFC.Game.Domain.Entities.Identity;
using SFC.Game.Infrastructure.Persistence.Configurations.Base;

namespace SFC.Game.Infrastructure.Persistence.Configurations.Game.Player;
public class GamePlayerConfiguration : AuditableEntityConfiguration<GamePlayer, long>
{
    public override void Configure(EntityTypeBuilder<GamePlayer> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasIndex(gp => new { gp.GameId, gp.PlayerId })
               .IsUnique();

        builder.HasOne<GamePlayerStatus>()
               .WithMany()
               .HasForeignKey(t => t.StatusId)
               .IsRequired(true);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(t => t.UserId)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.ClientCascade);

        builder.ToTable("Players");

        base.Configure(builder);
    }
}