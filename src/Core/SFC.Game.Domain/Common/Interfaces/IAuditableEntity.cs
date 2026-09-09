namespace SFC.Game.Domain.Common.Interfaces;

/// <summary>
/// Used for entities, which required auditable properties.
/// In general this is core entity of the service.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedDate { get; set; }

    Guid CreatedBy { get; set; }

    DateTime LastModifiedDate { get; set; }

    Guid LastModifiedBy { get; set; }
}