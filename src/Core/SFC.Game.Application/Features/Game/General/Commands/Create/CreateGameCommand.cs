using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.General.Commands.Create;
public class CreateGameCommand : Request<CreateGameViewModel>
{
    public override RequestId RequestId { get => RequestId.CreateGame; }

    public CreateGameDto Game { get; set; } = null!;
}