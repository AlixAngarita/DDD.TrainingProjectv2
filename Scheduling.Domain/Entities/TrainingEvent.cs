using Scheduling.Domain.Events;
using Scheduling.Domain.Exceptions;
using Scheduling.Domain.Values;

namespace Scheduling.Domain.Entities;


// A scheduled training event and the aggregate root of this model.

public class TrainingEvent : AggregateRoot
{
    private readonly List<InstructorAssignment> instructorAssignments;
    private readonly List<TraineeAssignment> traineeAssignments;
    private readonly List<ObserverAssignment> observerAssignments;
    
    public IReadOnlyCollection<InstructorAssignment> InstructorAssignments => instructorAssignments.AsReadOnly();
    public IReadOnlyCollection<TraineeAssignment> TraineeAssignments => traineeAssignments.AsReadOnly();
    public IReadOnlyCollection<ObserverAssignment> ObserverAssignments => observerAssignments.AsReadOnly();
    
    public Guid Id { get; }
    public Period Period { get; }
    public Title Name { get; }
    public Guid ProjectId { get; }
    private ETrainingEventStatus Status { get; set; }

    private TrainingEvent(Guid id, Guid projectId, Title name, Period period)
    {
        traineeAssignments = [];
        instructorAssignments = [];
        observerAssignments = [];
        
        Id = id;
        ProjectId = projectId;
        Name = name;
        Period = period;
        Status = ETrainingEventStatus.Normal;
    }
    
    public static TrainingEvent Create(Guid projectId, Title name, Period period)
    {
        var trainingEvent = new TrainingEvent(Guid.NewGuid(), projectId, name, period);
        trainingEvent.AddDomainEvent(new TrainingEventCreated(trainingEvent.Id, projectId));
        return trainingEvent;
    }

    public InstructorAssignment AssignInstructor(Guid resourceId)
    {
        if (Status != ETrainingEventStatus.Normal)
            throw new DomainException("Cannot assign an instructor to a cancelled training event.");
        
        if (HasInstructorWithResource(resourceId))
            throw new DomainException("An instructor cannot be assigned more than once per event.");
        
        var instructorAssignment = InstructorAssignment.Create(resourceId);
        instructorAssignments.Add(instructorAssignment);
        AddDomainEvent(new InstructorAssigned(instructorAssignment));
        return instructorAssignment;
    }

    public void UnassignInstructor(InstructorAssignment assignment)
    {
        if (!instructorAssignments.Remove(assignment))
            throw new ObjectNotFoundException($"The instructor with id: {assignment.Id} does not exist.");
        
        AddDomainEvent(new InstructorUnassigned(assignment));
    }
    
    public TraineeAssignment AssignTrainee(Guid resourceId)
    {
        if (Status != ETrainingEventStatus.Normal)
            throw new DomainException("Cannot assign a trainee to a cancelled training event.");
        
        if (HasTraineeWithResource(resourceId))
            throw new DomainException("A trainee cannot be assigned more than once per event.");
        
        var traineeAssignment = TraineeAssignment.Create(resourceId);
        traineeAssignments.Add(traineeAssignment);
        AddDomainEvent(new TraineeAssigned(traineeAssignment));
        return traineeAssignment;
    }

    public void UnassignTrainee(TraineeAssignment assignment)
    {
        if (!traineeAssignments.Remove(assignment))
            throw new ObjectNotFoundException($"The trainee with id: {assignment.Id} does not exist.");
        
        AddDomainEvent(new TraineeUnassigned(assignment));
    }
    
    public ObserverAssignment AssignObserver(Guid resourceId)
    {
        if (Status != ETrainingEventStatus.Normal)
            throw new DomainException("Cannot assign an observer to a cancelled training event.");
        
        if (HasObserverWithResource(resourceId))
            throw new DomainException("An observer cannot be assigned more than once per event.");
        
        var observerAssignment = ObserverAssignment.Create(resourceId);
        observerAssignments.Add(observerAssignment);
        AddDomainEvent(new ObserverAssigned(observerAssignment));
        return observerAssignment;
    }

    public void UnassignObserver(ObserverAssignment assignment)
    {
        if (!observerAssignments.Remove(assignment))
            throw new ObjectNotFoundException($"The observer with id: {assignment.Id} does not exist.");
        
        AddDomainEvent(new ObserverUnassigned(assignment));
    }
    
    public void Cancel()
    {
        Status = ETrainingEventStatus.Cancelled;
        AddDomainEvent(new TrainingEventCancelled(Id));
    }
    
    public bool HasInstructorWithResource(Guid resourceId) => instructorAssignments.Any(a => a.ResourceId == resourceId);
    public bool HasTraineeWithResource(Guid resourceId) => traineeAssignments.Any(a => a.ResourceId == resourceId);
    public bool HasObserverWithResource(Guid resourceId) => observerAssignments.Any(a => a.ResourceId == resourceId);
}

public enum ETrainingEventStatus
{
    Normal,
    Cancelled,
    Deleted
}