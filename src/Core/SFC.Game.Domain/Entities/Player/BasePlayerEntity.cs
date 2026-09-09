using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Player;
public abstract class BasePlayerEntity : BaseEntity<long>
{
    public Player Player { get; set; } = null!;
}