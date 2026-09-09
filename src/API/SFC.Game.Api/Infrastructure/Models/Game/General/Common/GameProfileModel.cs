using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// Game **profile** model.
/// </summary>
public class GameProfileModel : IMapFromReverse<GameProfileDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public required GameGeneralProfileModel General { get; set; }

    /// <summary>
    /// Financial profile.
    /// </summary>
    public required GameFinancialProfileModel Financial { get; set; }

    /// <summary>
    /// Inventary profile.
    /// </summary>
    public required GameInventaryProfileModel Inventary { get; set; }
}