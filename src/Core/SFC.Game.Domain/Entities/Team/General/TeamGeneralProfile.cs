namespace SFC.Game.Domain.Entities.Team.General;
public class TeamGeneralProfile : BaseTeamEntity
{
    public required string Name { get; set; }

    public required string City { get; set; }

    public long? LocationId { get; set; }

    public string? Description { get; set; }
}