namespace SFC.Game.Domain.Entities.Game.General;
public class GameGeneralProfile : BaseGameEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public long? LocationId { get; set; }
}