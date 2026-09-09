using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Queries.Find.Dto.Filters;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Find.Filters;

/// <summary>
/// Get teams **general profile filter** model.
/// </summary>
public class GetGamesGeneralProfileFilterModel : IMapTo<GetGamesGeneralProfileFilterDto>
{
    /// <summary>
    /// Name of game.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Game's **tags**.
    /// </summary>
    public IEnumerable<string>? Tags { get; set; }

    /// <summary>
    /// Game's **availability** model.
    /// </summary>
    public GetGamesAvailabilityLimitModel? Availability { get; set; }

    /// <summary>
    /// **Location** where game will play.
    /// </summary>
    public long? Location { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<GetGamesGeneralProfileFilterModel, GetGamesGeneralProfileFilterDto>()
                                                   .ForMember(p => p.LocationId, d => d.MapFrom(z => z.Location));
}