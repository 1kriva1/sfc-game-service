using SFC.Game.Messages.Models.Game.Team.Player;

namespace SFC.Game.Messages.Events.Game.Team.Player;
public class GameTeamPlayerDeleted
{
    public required GameTeamPlayer GameTeamPlayer { get; set; }
}