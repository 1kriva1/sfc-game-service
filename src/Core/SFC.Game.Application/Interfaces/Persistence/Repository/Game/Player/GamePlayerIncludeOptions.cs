namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Player;

[Flags]
public enum GamePlayerIncludeOptions
{
    None = 0,
    Player = 1 << 0,
    PlayerProfile = 1 << 1,
    GameTeamPlayer = 1 << 2,
    GameTeamPlayerWithGameTeam = 1 << 3,
    GameTeamPlayerWithGameTeamWithTeam = 1 << 4,
    GameTeamPlayerWithGameTeamWithTeamProfile = 1 << 5,
    GameTeamPlayerWithGameTeamWithTeamWithTeamPlayers = 1 << 6,
    GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayer = 1 << 7,
    GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile = 1 << 8,
    All = PlayerProfile |
        GameTeamPlayerWithGameTeamWithTeamProfile |
        GameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile
}