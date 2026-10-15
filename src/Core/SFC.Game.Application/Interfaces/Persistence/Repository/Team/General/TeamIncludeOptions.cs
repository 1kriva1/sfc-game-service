namespace SFC.Game.Application.Interfaces.Persistence.Repository.Team.General;

[Flags]
public enum TeamIncludeOptions
{
    None = 0,
    Profile = 1 << 0,
    TeamPlayers = 1 << 1,
    TeamPlayersWithPlayer = 1 << 2,
    TeamPlayersWithPlayerProfile = 1 << 3,
}