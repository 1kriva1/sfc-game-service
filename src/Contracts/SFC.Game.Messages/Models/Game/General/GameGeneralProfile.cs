namespace SFC.Game.Messages.Models.Game.General;
public class GameGeneralProfile
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public long? LocationId { get; set; }
}