using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// Game's **inventary** profile model.
/// </summary>
public class GameInventaryProfileModel : IMapFromReverse<GameInventaryProfileDto>
{
    /// <summary>
    /// Is it required to have shirts for play.
    /// </summary>
    public bool ShirtsRequired { get; set; }

    /// <summary>
    /// How many shirts required for game.
    /// </summary>
    public int? ShirtsCount { get; set; }
}