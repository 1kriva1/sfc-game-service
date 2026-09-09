namespace SFC.Game.Application.Common.Dto.Team.General.Filters;
public class TeamGeneralProfileFilterDto
{
    public string? Name { get; set; }

    public string? City { get; set; }

    public IEnumerable<string>? Tags { get; set; }

    public TeamAvailabilityLimitDto? Availability { get; set; }

    public long? LocationId { get; set; }

    public bool? HasLogo { get; set; }
}