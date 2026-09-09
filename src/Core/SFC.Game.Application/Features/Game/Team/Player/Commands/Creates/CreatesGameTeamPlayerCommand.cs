using SFC.Game.Application.Common.Enums;
using SFC.Game.Application.Features.Common.Base;

namespace SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;

public class CreatesGameTeamPlayerCommand : Request<CreatesGameTeamPlayerViewModel>
{
    public override RequestId RequestId { get => RequestId.CreatesGameTeamPlayer; }

    public IEnumerable<CreatesGameTeamPlayerDto> GameTeamPlayers { get; set; } = [];

    public CreatesGameTeamPlayerCommand SetGameId(long gameId)
    {
        foreach (CreatesGameTeamPlayerDto item in GameTeamPlayers)
        {
            item.GameId = gameId;
        }

        return this;
    }

    public CreatesGameTeamPlayerCommand SetTeamId(long teamId)
    {
        foreach (CreatesGameTeamPlayerDto item in GameTeamPlayers)
        {
            item.TeamId = teamId;
        }

        return this;
    }
}