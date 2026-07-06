using Scheduling.Domain.Events;

namespace Scheduling.Domain.Entities;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> domainEvents = new();
    public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(DomainEvent domainEvent) => domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => domainEvents.Clear();
}