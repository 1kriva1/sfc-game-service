using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Application.Features.Game.General.Common.Dto;
public class GameFinancialProfileDto : IMapFromReverse<GameFinancialProfile>
{
    public bool FreeGame { get; set; }

    public decimal? PayAmount { get; set; }
}