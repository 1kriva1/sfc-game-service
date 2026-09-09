using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Create;
public class CreateGameTeamCommand : Request<CreateGameTeamViewModel>
{
    public override RequestId RequestId { get => RequestId.CreateGameTeam; }

    public required CreateGameTeamDto GameTeam { get; set; }

    public CreateGameTeamCommand SetGameId(long gameId)
    {
        GameTeam.GameId = gameId;
        return this;
    }
}