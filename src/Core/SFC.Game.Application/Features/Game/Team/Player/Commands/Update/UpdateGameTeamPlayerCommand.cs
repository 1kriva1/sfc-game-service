using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Update;
public class UpdateGameTeamPlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdateGameTeamPlayer; }

    public required UpdateGameTeamPlayerDto GameTeamPlayer { get; set; }

    public UpdateGameTeamPlayerCommand SetGameId(long gameId)
    {
        GameTeamPlayer.GameId = gameId;
        return this;
    }

    public UpdateGameTeamPlayerCommand SetPlayerId(long playerId)
    {
        GameTeamPlayer.PlayerId = playerId;
        return this;
    }

    public UpdateGameTeamPlayerCommand SetTeamId(long teamId)
    {
        GameTeamPlayer.TeamId = teamId;
        return this;
    }
}