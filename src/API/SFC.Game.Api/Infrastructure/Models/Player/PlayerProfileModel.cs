using SFC.Game.Application.Common.Dto.Player.General;
using SFC.Game.Application.Common.Mappings.Interfaces;

namespace SFC.Game.Api.Infrastructure.Models.Player;

/// <summary>
/// Player **profile** model for get players request.
/// </summary>
public class PlayerProfileModel : IMapFrom<PlayerProfileDto>
{
    /// <summary>
    /// General profile.
    /// </summary>
    public PlayerGeneralProfileModel General { get; set; } = null!;

    /// <summary>
    /// Football profile.
    /// </summary>
    public PlayerFootballProfileModel Football { get; set; } = null!;
}