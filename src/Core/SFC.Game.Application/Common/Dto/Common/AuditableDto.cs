namespace SFC.Game.Application.Common.Dto.Common;

/// <summary>
/// Base DTO class for auditable entities.
/// </summary>
public class AuditableDto
{
    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public Guid LastModifiedBy { get; set; }
}