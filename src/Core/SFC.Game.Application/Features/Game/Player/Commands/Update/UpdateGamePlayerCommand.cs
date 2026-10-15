using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Player.Commands.Update;
public class UpdateGamePlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdateGamePlayer; }

    public required UpdateGamePlayerDto GamePlayer { get; set; }
}