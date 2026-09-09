using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.General.Commands.Update;
public class UpdateGameCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdateGame; }

    public long GameId { get; set; }

    public required UpdateGameDto Game { get; set; }

    public UpdateGameCommand SetGameId(long id)
    {
        GameId = id;
        return this;
    }
}