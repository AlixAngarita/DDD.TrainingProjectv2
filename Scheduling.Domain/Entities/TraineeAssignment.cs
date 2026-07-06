namespace Scheduling.Domain.Entities;

public class TraineeAssignment : IAssignment<TraineeAssignment>
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    private TraineeAssignment(Guid id, Guid resourceId)
    {
        Id = id;
        ResourceId = resourceId;
    }

    public static TraineeAssignment Create(Guid resourceId)
    {
        return new TraineeAssignment(Guid.NewGuid(), resourceId);
    }
}