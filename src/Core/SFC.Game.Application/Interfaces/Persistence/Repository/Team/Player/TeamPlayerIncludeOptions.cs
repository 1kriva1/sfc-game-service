namespace SFC.Game.Application.Interfaces.Persistence.Repository.Team.Player;

[Flags]
public enum TeamPlayerIncludeOptions
{
    None = 0,
    Player = 1 << 0,
    PlayerProfile = 1 << 1,
}