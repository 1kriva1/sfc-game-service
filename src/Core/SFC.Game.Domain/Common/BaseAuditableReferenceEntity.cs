using SFC.Game.Domain.Common.Interfaces;

namespace SFC.Game.Domain.Common;

/// <summary>
/// For core entities from other services.
/// </summary>
/// <typeparam name="TId">Type for entity identifier.</typeparam>
public abstract class BaseAuditableReferenceEntity<TId> : BaseEntity<TId>, IAuditableReferenceEntity
{
    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public Guid LastModifiedBy { get; set; }
}