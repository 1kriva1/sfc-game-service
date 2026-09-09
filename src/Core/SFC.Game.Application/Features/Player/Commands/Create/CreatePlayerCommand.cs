using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Player.Commands.Create;
public class CreatePlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreatePlayer; }

    public PlayerDto Player { get; set; } = null!;
}