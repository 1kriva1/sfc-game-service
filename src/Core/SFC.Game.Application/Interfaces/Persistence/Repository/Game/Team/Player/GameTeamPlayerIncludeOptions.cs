namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.Player;

[Flags]
public enum GameTeamPlayerIncludeOptions
{
    None = 0,
    GamePlayer = 1 << 1,
    GamePlayerWithPlayer = 1 << 2,
    GamePlayerWithPlayerProfile = 1 << 3,
    TeamPlayer = 1 << 4,
    TeamPlayerWithPlayer = 1 << 5,
    TeamPlayerWithPlayerProfile = 1 << 6,
    GameTeam = 1 << 7,
    GameTeamWithTeam = 1 << 8,
    GameTeamWithTeamProfile = 1 << 9,
    GameTeamWithTeamWithTeamPlayers = 1 << 10,
    GameTeamWithTeamWithTeamPlayersWithPlayer = 1 << 11,
    GameTeamWithTeamWithTeamPlayersWithPlayerProfile = 1 << 12,
    GameTeamWithGameTeamPlayers = 1 << 13,
    GameTeamWithGameTeamPlayersWithGamePlayer = 1 << 14,
    GameTeamWithGameTeamPlayersWithGamePlayerWithPlayer = 1 << 15,
    GameTeamWithGameTeamPlayersWithGamePlayerWithPlayerProfile = 1 << 16,
    GameTeamWithGameTeamPlayersWithTeamPlayer = 1 << 17,
    GameTeamWithGameTeamPlayersWithTeamPlayerWithPlayer = 1 << 18,
    GameTeamWithGameTeamPlayersWithTeamPlayerWithPlayerProfile = 1 << 19,
    All =
        GamePlayerWithPlayerProfile |
        TeamPlayerWithPlayerProfile |
        GameTeamWithTeamProfile |
        GameTeamWithTeamWithTeamPlayersWithPlayerProfile
}