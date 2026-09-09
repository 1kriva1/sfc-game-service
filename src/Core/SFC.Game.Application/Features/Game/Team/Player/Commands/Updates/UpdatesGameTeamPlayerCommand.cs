using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;

public class UpdatesGameTeamPlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.UpdatesGameTeamPlayer; }

    public required IEnumerable<UpdatesGameTeamPlayerDto> GameTeamPlayers { get; set; }

    public UpdatesGameTeamPlayerCommand SetGameId(long gameId)
    {
        foreach (UpdatesGameTeamPlayerDto gameTeamPlayer in GameTeamPlayers)
        {
            gameTeamPlayer.GameId = gameId;
        }

        return this;
    }

    public UpdatesGameTeamPlayerCommand SetTeamId(long teamId)
    {
        foreach (UpdatesGameTeamPlayerDto gameTeamPlayer in GameTeamPlayers)
        {
            gameTeamPlayer.TeamId = teamId;
        }

        return this;
    }
}