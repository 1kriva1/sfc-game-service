using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Delete;
public class DeleteGameTeamPlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.DeleteGameTeamPlayer; }

    public long GameId { get; set; }

    public long TeamId { get; set; }

    public long PlayerId { get; set; }

    public DeleteGameTeamPlayerCommand SetGameId(long gameId)
    {
        GameId = gameId;
        return this;
    }

    public DeleteGameTeamPlayerCommand SetPlayerId(long playerId)
    {
        PlayerId = playerId;
        return this;
    }

    public DeleteGameTeamPlayerCommand SetTeamId(long teamId)
    {
        TeamId = teamId;
        return this;
    }
}