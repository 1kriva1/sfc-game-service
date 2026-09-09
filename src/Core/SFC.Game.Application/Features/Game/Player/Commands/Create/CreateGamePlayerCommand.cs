using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Player.Commands.Create;
public class CreateGamePlayerCommand : Request<CreateGamePlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.CreateGamePlayer; }

    public required CreateGamePlayerDto GamePlayer { get; set; }

    public CreateGamePlayerCommand SetGameId(long gameId)
    {
        GamePlayer.GameId = gameId;
        return this;
    }
}