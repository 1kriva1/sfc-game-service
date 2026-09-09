using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Game.General;

namespace SFC.Game.Application.Features.Game.General.Common.Dto;
public class GameInventaryProfileDto : IMapFromReverse<GameInventaryProfile>
{
    public bool ShirtsRequired { get; set; }

    public int? ShirtsCount { get; set; }
}