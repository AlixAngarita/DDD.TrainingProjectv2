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
    
    public Guid Id { get; private set; }
    public Period Period { get; private set; }
    public Title Name { get; private set; }
    public Guid ProjectId { get; private set; }
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
        Status = ETrainingEventStatus.Confirmed;
    }
    
    public static TrainingEvent Create(Guid projectId, Title name, Period period)
    {
        var trainingEvent = new TrainingEvent(Guid.NewGuid(), projectId, name, period);
        trainingEvent.AddDomainEvent(new TrainingEventCreated(trainingEvent.Id, projectId));
        return trainingEvent;
    }

    public InstructorAssignment AssignInstructor(Guid assigneeId)
    {
        MustBeConfirmedEvent();
        
        if (HasInstructorWithResource(assigneeId))
            throw new DomainException("An instructor cannot be assigned more than once per event.");
        
        var instructorAssignment = InstructorAssignment.Create(assigneeId);
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
    
    public TraineeAssignment AssignTrainee(Guid assigneeId)
    {
        MustBeConfirmedEvent();
        
        if (HasTraineeWithResource(assigneeId))
            throw new DomainException("A trainee cannot be assigned more than once per event.");
        
        var traineeAssignment = TraineeAssignment.Create(assigneeId);
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
    
    public ObserverAssignment AssignObserver(Guid assigneeId)
    {
        MustBeConfirmedEvent();
        
        if (HasObserverWithResource(assigneeId))
            throw new DomainException("An observer cannot be assigned more than once per event.");
        
        var observerAssignment = ObserverAssignment.Create(assigneeId);
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
    
    private void MustBeConfirmedEvent()
    {
        if (Status != ETrainingEventStatus.Confirmed)
            throw new DomainException("Cannot assign an observer to a cancelled training event.");
    }
    
    public bool HasInstructorWithResource(Guid assigneeId) => instructorAssignments.Any(a => a.AssigneeId == assigneeId);
    public bool HasTraineeWithResource(Guid assigneeId) => traineeAssignments.Any(a => a.AssigneeId == assigneeId);
    public bool HasObserverWithResource(Guid assigneeId) => observerAssignments.Any(a => a.AssigneeId == assigneeId);
}

public enum ETrainingEventStatus
{
    Confirmed,
    Cancelled,
    Deleted
}