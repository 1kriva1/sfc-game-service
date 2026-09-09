using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Application.Features.Game.General.Common.Dto;

namespace SFC.Game.Api.Infrastructure.Models.Game.General.Common;

/// <summary>
/// Game's **financial** profile model.
/// </summary>
public class GameFinancialProfileModel : IMapFromReverse<GameFinancialProfileDto>
{
    /// <summary>
    /// Game play only on free field and without any extra expansions.
    /// </summary>
    public bool FreeGame { get; set; }

    /// <summary>
    /// How many need to pay for game.
    /// </summary>
    public decimal? PayAmount { get; set; }
}