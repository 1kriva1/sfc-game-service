using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Common.Extensions;
public static class GameTeamPlayerExtensions
{
    public static GameTeamPlayer SetGameTeamId(this GameTeamPlayer value, long gameTeamId)
    {
        value.GameTeamId = gameTeamId;
        return value;
    }

    public static GameTeamPlayer SetGamePlayerId(this GameTeamPlayer value, long? gamePlayerId)
    {
        value.GamePlayerId = gamePlayerId;
        return value;
    }

    public static GameTeamPlayer SetTeamPlayerId(this GameTeamPlayer value, long? teamPlayerId)
    {
        value.TeamPlayerId = teamPlayerId;
        return value;
    }
}