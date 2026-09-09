namespace SFC.Game.Domain.Common.Interfaces;

/// <summary>
/// Main interface for entities.
/// </summary>
public interface IEntity
{
    IReadOnlyCollection<BaseEvent> DomainEvents { get; }

    void AddDomainEvent(BaseEvent domainEvent);

    void RemoveDomainEvent(BaseEvent domainEvent);

    void ClearDomainEvents();
}