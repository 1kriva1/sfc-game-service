namespace SFC.Game.Domain.Entities.Team.General;
public class TeamAvailability : BaseTeamEntity
{
    public DayOfWeek Day { get; set; }

    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}