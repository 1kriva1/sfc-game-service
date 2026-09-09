namespace SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;
public class GetGamesGeneralProfileFilterDto
{
    public string? Name { get; set; }

    public IEnumerable<string>? Tags { get; set; }

    public GetGamesAvailabilityLimitDto? Availability { get; set; }

    public long? LocationId { get; set; }
}