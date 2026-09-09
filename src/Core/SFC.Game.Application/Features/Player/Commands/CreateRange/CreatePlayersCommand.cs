using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Player.Commands.CreateRange;
public class CreatePlayersCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.CreatePlayers; }

    public IEnumerable<PlayerDto> Players { get; set; } = null!;
}