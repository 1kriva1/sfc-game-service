using SFC.Game.Domain.Common.Interfaces;

namespace SFC.Game.Domain.Common;

/// <summary>
/// For core entities of the service.
/// </summary>
/// <typeparam name="TId">Type for entity identifier.</typeparam>
public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
{
    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public Guid LastModifiedBy { get; set; }
}