using System.ComponentModel;

namespace SFC.Game.Domain.Enums.Game;

public enum GamePlayerStatus
{
    [Description("In Game")]
    InGame = 0,
    [Description("Out Of Game")]
    OutOfGame = 1
}