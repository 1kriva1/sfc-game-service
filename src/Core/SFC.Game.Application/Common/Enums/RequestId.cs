namespace SFC.Game.Application.Common.Enums;
public enum RequestId
{
    // main
    DatabaseReset,
    // data
    InitData,
    ResetData,
    // identity
    CreateUser,
    CreateUsers,
    // player
    CreatePlayer,
    UpdatePlayer,
    CreatePlayers,
    // team
    ResetTeamData,
    CreateTeam,
    UpdateTeam,
    CreateTeams,
    // team player
    CreateTeamPlayer,
    UpdateTeamPlayer,
    CreateTeamPlayers,
    // invite
    ResetInviteData,
    // request
    ResetRequestData,
    // game
    GetAllGameData,
    CreateGame,
    UpdateGame,
    GetGame,
    GetGames,
    // game team,
    GetGameTeam,
    GetsGameTeam,
    CreateGameTeam,
    UpdateGameTeam,
    UpdatesGameTeam,
    GetGameTeams,
    // game team player,
    GetGameTeamPlayer,
    GetGameTeamPlayers,
    CreateGameTeamPlayer,
    CreatesGameTeamPlayer,
    UpdateGameTeamPlayer,
    UpdatesGameTeamPlayer,
    // game player
    GetGamePlayer,
    CreateGamePlayer,
    UpdateGamePlayer,
    GetGamePlayers,
}