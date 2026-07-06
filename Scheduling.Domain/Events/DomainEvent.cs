namespace Scheduling.Domain.Events;

// Base type for domain events

public abstract record DomainEvent
{
    // The instant or timestamp of the domain event
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}