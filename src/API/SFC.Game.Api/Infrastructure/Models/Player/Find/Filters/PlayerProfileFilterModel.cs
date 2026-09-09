using SFC.Game.Application.Common.Dto.Player.General.Filters;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player.Find.Filters;

/// <summary>
/// Get players **profile filter** model.
/// </summary>
public class PlayerProfileFilterModel : IMapTo<PlayerProfileFilterDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public PlayerGeneralProfileFilterModel General { get; set; } = default!;

    /// <summary>
    /// Football profile.
    /// </summary>
    public PlayerFootballProfileFilterModel Football { get; set; } = default!;
}