namespace Scheduling.Domain.Entities;

public abstract class Assignment
{
    public Guid Id { get; private set; }
    public Guid AssigneeId { get; private set; }
    
    protected Assignment(Guid id, Guid assigneeId)
    {
        Id = id;
        AssigneeId = assigneeId;
    }
}