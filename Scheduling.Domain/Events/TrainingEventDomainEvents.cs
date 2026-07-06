using Scheduling.Domain.Entities;
using Scheduling.Domain.Values;

namespace Scheduling.Domain.Events;

// Domain events raised by the TrainingEvent aggregate root.
//
//   Lifecycle:   TrainingEventCreated, TrainingEventCancelled
//   Assignments: ResourceAssigned, ResourceUnassigned

public record TrainingEventCreated(Guid EventId, Guid ProjectId) : DomainEvent;

public record TrainingEventCancelled(Guid EventId) : DomainEvent;

public record InstructorAssigned(InstructorAssignment Assignment) : DomainEvent;

public record InstructorUnassigned(InstructorAssignment Assignment) : DomainEvent;
    
public record TraineeAssigned(TraineeAssignment Assignment) : DomainEvent;

public record TraineeUnassigned(TraineeAssignment Assignment) : DomainEvent;
    
public record ObserverAssigned(ObserverAssignment Assignment) : DomainEvent;

public record ObserverUnassigned(ObserverAssignment Assignment) : DomainEvent;