namespace SFC.Game.Domain.Common.Interfaces;

/// <summary>
/// Entities that need user link.
/// </summary>
public interface IUserEntity
{
    Guid UserId { get; set; }
}