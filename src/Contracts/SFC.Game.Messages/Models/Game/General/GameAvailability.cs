namespace SFC.Game.Messages.Models.Game.General;
public class GameAvailability
{
    public DateOnly Date { get; set; }

    public TimeSpan From { get; set; }

    public TimeSpan To { get; set; }
}