namespace Scheduling.Domain.Entities;

public class ObserverAssignment : IAssignment<ObserverAssignment>
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    private ObserverAssignment(Guid id, Guid resourceId)
    {
        Id = id;
        ResourceId = resourceId;
    }

    public static ObserverAssignment Create(Guid resourceId)
    {
        return new ObserverAssignment(Guid.NewGuid(), resourceId);
    }
}