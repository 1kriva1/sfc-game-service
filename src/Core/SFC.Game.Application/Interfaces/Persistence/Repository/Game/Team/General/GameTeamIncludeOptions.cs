namespace SFC.Game.Application.Interfaces.Persistence.Repository.Game.Team.General;

[Flags]
public enum GameTeamIncludeOptions
{
    None = 0,
    Team = 1 << 1,
    TeamProfile = 1 << 2,
    TeamWithTeamPlayers = 1 << 3,
    TeamWithTeamPlayersWithPlayer = 1 << 4,
    TeamWithTeamPlayersWithPlayerProfile = 1 << 5,
    GameTeamPlayers = 1 << 6,
    GameTeamPlayersWithGamePlayer = 1 << 7,
    GameTeamPlayersWithGamePlayerWithPlayer = 1 << 8,
    GameTeamPlayersWithGamePlayerWithPlayerProfile = 1 << 9,
    GameTeamPlayersWithTeamPlayer = 1 << 10,
    GameTeamPlayersWithTeamPlayerWithPlayer = 1 << 11,
    GameTeamPlayersWithTeamPlayerWithPlayerProfile = 1 << 12,
    All = TeamProfile |
          TeamWithTeamPlayersWithPlayerProfile |
          GameTeamPlayersWithGamePlayerWithPlayerProfile |
          GameTeamPlayersWithTeamPlayerWithPlayerProfile
}