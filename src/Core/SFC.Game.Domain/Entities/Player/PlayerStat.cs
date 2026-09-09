using SFC.Game.Domain.Entities.Data;

namespace SFC.Game.Domain.Entities.Player;
public class PlayerStat : BasePlayerEntity
{
    public StatTypeEnum TypeId { get; set; }

    public required StatType Type { get; set; }

    public byte Value { get; set; }
}