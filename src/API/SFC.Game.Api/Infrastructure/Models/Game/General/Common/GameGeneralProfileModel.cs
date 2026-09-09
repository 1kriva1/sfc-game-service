using AutoMapper;

using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// Game's **general** profile model.
/// </summary>
public class GameGeneralProfileModel : IMapFromReverse<GameGeneralProfileDto>
{
    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// A few words about game.
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Location of main game field.
    /// </summary>
    public long? Location { get; set; }

    /// <summary>
    /// Game's **availability** to play.
    /// </summary>
    public required GameAvailabilityModel Availability { get; set; }

    /// <summary>
    /// Game's **tags**.
    /// </summary>
    public IEnumerable<string>? Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GameGeneralProfileDto, GameGeneralProfileModel>()
               .ForMember(p => p.Location, d => d.MapFrom(z => z.LocationId))
               .ReverseMap();
    }
}