using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Create;
public class CreateGameTeamPlayerCommand : Request<CreateGameTeamPlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.CreateGameTeamPlayer; }

    public required CreateGameTeamPlayerDto GameTeamPlayer { get; set; }

    public CreateGameTeamPlayerCommand SetGameId(long gameId)
    {
        GameTeamPlayer.GameId = gameId;
        return this;
    }

    public CreateGameTeamPlayerCommand SetPlayerId(long playerId)
    {
        GameTeamPlayer.PlayerId = playerId;
        return this;
    }

    public CreateGameTeamPlayerCommand SetTeamId(long teamId)
    {
        GameTeamPlayer.TeamId = teamId;
        return this;
    }
}