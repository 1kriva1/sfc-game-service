using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Player;
public class PlayerTag : BasePlayerEntity
{
    public string Value { get; set; } = null!;
}