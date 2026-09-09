using SFC.Game.Domain.Common;

namespace SFC.Game.Domain.Entities.Data;
public class WorkingFoot : EnumDataEntity<WorkingFootEnum>
{
    public WorkingFoot() : base() { }

    public WorkingFoot(WorkingFootEnum enumType) : base(enumType) { }
}