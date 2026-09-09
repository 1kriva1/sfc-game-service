using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Player.Commands.Update;
public class UpdatePlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdatePlayer; }

    public PlayerDto Player { get; set; } = null!;
}