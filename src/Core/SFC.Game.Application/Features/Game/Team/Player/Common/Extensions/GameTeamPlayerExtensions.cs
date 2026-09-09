using System.Net.NetworkInformation;

using SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;
using SFC.Game.Domain.Entities.Game.Team.General;
using SFC.Game.Domain.Entities.Game.Team.Player;

namespace SFC.Game.Application.Features.Game.Team.Player.Common.Extensions;
public static class GameTeamPlayerExtensions
{
    public static GameTeamPlayer SetStatus(this GameTeamPlayer value, TeamPlayerStatusEnum status)
    {
        value.StatusId = status;
        return value;
    }

    public static IEnumerable<GameTeamPlayer> SetStatus(this IEnumerable<GameTeamPlayer> value, TeamPlayerStatusEnum status)
    {
        foreach (GameTeamPlayer item in value)
        {
            item.SetStatus(status);
        }

        return value;
    }

    public static GameTeamPlayer SetGameTeamId(this GameTeamPlayer value, long gameTeamId)
    {
        value.GameTeamId = gameTeamId;
        return value;
    }
}