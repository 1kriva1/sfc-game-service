namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.General;

[Flags]
public enum GameIncludeOptions
{
    None = 0,
    Profile = 1 << 0,
    // game teams
    GameTeams = 1 << 1,
    GameTeamsWithTeam = 1 << 2,
    GameTeamsWithTeamProfile = 1 << 3,
    GameTeamsWithTeamWithTeamPlayers = 1 << 4,
    GameTeamsWithTeamWithTeamPlayersWithPlayer = 1 << 5,
    GameTeamsWithTeamWithTeamPlayersWithPlayerProfile = 1 << 6,
    GameTeamsWithGameTeamPlayers = 1 << 7,
    GameTeamsWithGameTeamPlayersWithGamePlayer = 1 << 8,
    GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayer = 1 << 9,
    GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayerProfile = 1 << 10,
    GameTeamsWithGameTeamPlayersWithTeamPlayer = 1 << 11,
    GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayer = 1 << 12,
    GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayerProfile = 1 << 13,
    // game players
    GamePlayers = 1 << 14,
    GamePlayersWithPlayer = 1 << 15,
    GamePlayersWithPlayerProfile = 1 << 16,
    GamePlayersWithGameTeamPlayer = 1 << 17,
    GamePlayersWithGameTeamPlayerWithGameTeam = 1 << 18,
    GamePlayersWithGameTeamPlayerWithGameTeamWithTeam = 1 << 19,
    GamePlayersWithGameTeamPlayerWithGameTeamWithTeamProfile = 1 << 20,
    GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayers = 1 << 21,
    GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayer = 1 << 22,
    GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile = 1 << 23,
    All = Profile |
        GameTeamsWithTeamProfile |
        GameTeamsWithTeamWithTeamPlayersWithPlayerProfile |
        GameTeamsWithGameTeamPlayersWithGamePlayerWithPlayerProfile |
        GameTeamsWithGameTeamPlayersWithTeamPlayerWithPlayerProfile |
        GamePlayersWithPlayerProfile |
        GamePlayersWithGameTeamPlayerWithGameTeamWithTeamProfile |
        GamePlayersWithGameTeamPlayerWithGameTeamWithTeamWithTeamPlayersWithPlayerProfile
}