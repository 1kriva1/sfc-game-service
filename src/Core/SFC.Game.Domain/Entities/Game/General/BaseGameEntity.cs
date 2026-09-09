using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Game.General;
public abstract class BaseGameEntity : BaseEntity<long>
{
    public GameEntity Game { get; set; } = null!;
}