namespace Scheduling.Domain.Entities;

public class ObserverAssignment : Assignment
{
    private ObserverAssignment(Guid id, Guid assigneeId) : base(id, assigneeId) { }

    public static ObserverAssignment Create(Guid assigneeId)
    {
        return new ObserverAssignment(Guid.NewGuid(), assigneeId);
    }
}