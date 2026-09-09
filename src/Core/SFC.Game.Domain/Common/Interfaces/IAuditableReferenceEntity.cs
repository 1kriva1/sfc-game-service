namespace SFC.Game.Domain.Common.Interfaces;

/// <summary>
/// Used for entities, which required auditable properties from other services.
/// </summary>
public interface IAuditableReferenceEntity
{
    DateTime CreatedDate { get; set; }

    Guid CreatedBy { get; set; }

    DateTime LastModifiedDate { get; set; }

    Guid LastModifiedBy { get; set; }
}