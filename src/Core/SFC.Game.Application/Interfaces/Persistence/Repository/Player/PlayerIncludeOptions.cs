namespace SFC.Game.Application.Interfaces.Persistence.Repository.Player;

[Flags]
public enum PlayerIncludeOptions
{
    None = 0,
    Profile = 1 << 0,
}