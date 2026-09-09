using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Update;
public class UpdateGameTeamCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdateGameTeam; }

    public required UpdateGameTeamDto GameTeam { get; set; }

    public UpdateGameTeamCommand SetGameId(long gameId)
    {
        GameTeam.GameId = gameId;
        return this;
    }

    public UpdateGameTeamCommand SetTeamId(long teamId)
    {
        GameTeam.TeamId = teamId;
        return this;
    }
}