namespace SFC.Game.Domain.Common.Interfaces;

/// <summary>
/// Entities from Data service are special.
/// They are required only date of creation.
/// </summary>
public interface IDataEntity
{
    public DateTime CreatedDate { get; set; }
}