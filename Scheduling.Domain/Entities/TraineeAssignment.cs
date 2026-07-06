namespace Scheduling.Domain.Entities;

public class TraineeAssignment : Assignment
{
    private TraineeAssignment(Guid id, Guid assigneeId) : base(id, assigneeId) { }

    public static TraineeAssignment Create(Guid assigneeId)
    {
        return new TraineeAssignment(Guid.NewGuid(), assigneeId);
    }
}