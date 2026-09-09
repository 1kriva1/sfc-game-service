using AutoMapper;

using SFC.Game.Application.Common.Dto.Team.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Team.General.Filters;

/// <summary>
/// Team **general profile filter** model.
/// </summary>
public class TeamGeneralProfileFilterModel : IMapTo<TeamGeneralProfileFilterDto>
{
    /// <summary>
    /// Name of team.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// **City** where team will play football.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Team's **tags**.
    /// </summary>
    public IEnumerable<string>? Tags { get; set; }

    /// <summary>
    /// Team's **availability** model.
    /// </summary>
    public TeamAvailabilityLimitModel? Availability { get; set; }

    /// <summary>
    /// **Location** where team mostly will play football.
    /// </summary>
    public long? Location { get; set; }

    /// <summary>
    /// Describe if team must have uploaded logo.
    /// </summary>
    public bool? HasLogo { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<TeamGeneralProfileFilterModel, TeamGeneralProfileFilterDto>()
                                                   .ForMember(p => p.LocationId, d => d.MapFrom(z => z.Location));
}