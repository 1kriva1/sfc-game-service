namespace SFC.Game.Domain.Entities.Game.General;
public class GameAvailability : BaseGameEntity
{
    public DateOnly Date { get; set; }

    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}