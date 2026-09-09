using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Team.General.Commands.Updates;

public class UpdatesGameTeamCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdatesGameTeam; }

    public required IEnumerable<UpdatesGameTeamDto> GameTeams { get; set; }

    public UpdatesGameTeamCommand SetGameId(long gameId)
    {
        foreach (UpdatesGameTeamDto gameTeam in GameTeams)
        {
            gameTeam.GameId = gameId;
        }

        return this;
    }
}