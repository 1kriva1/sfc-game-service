using SFC.Game.Domain.Common.Interfaces;

namespace SFC.Game.Domain.Common;
public class DataEntity<TId> : BaseEntity<TId>, IDataEntity
{
    public DateTime CreatedDate { get; set; }
}