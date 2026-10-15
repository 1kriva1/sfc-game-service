using SFC.Game.Application.Common.Enums;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Deletes;

public class DeletesGameTeamPlayerCommand : ParentRequest
{
    public override RequestId RequestId { get => RequestId.DeletesGameTeamPlayer; }

    public required IEnumerable<DeletesGameTeamPlayerDto> GameTeamPlayers { get; set; }

    public DeletesGameTeamPlayerCommand SetGameId(long gameId)
    {
        foreach (DeletesGameTeamPlayerDto gameTeamPlayer in GameTeamPlayers)
        {
            gameTeamPlayer.GameId = gameId;
        }

        return this;
    }

    public DeletesGameTeamPlayerCommand SetTeamId(long teamId)
    {
        foreach (DeletesGameTeamPlayerDto gameTeamPlayer in GameTeamPlayers)
        {
            gameTeamPlayer.TeamId = teamId;
        }

        return this;
    }
}