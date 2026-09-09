using SFC.Game.Messages.Models.Game.Team.General;

namespace SFC.Game.Messages.Events.Game.Team.General;
public class GameTeamUpdated
{
    public required GameTeam GameTeam { get; set; }
}