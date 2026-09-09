using SFC.Game.Application.Common.Dto.Data;
using SFC.Game.Application.Common.Mappings.Interfaces;
using SFC.Game.Domain.Entities.Data;

namespace SFC.Game.Application.Features.Data.Common.Dto;
public class StatTypeDto : DataDto, IMapTo<StatType>
{
    public int CategoryId { get; set; }

    public int SkillId { get; set; }
}