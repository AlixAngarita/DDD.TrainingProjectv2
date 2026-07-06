namespace Scheduling.Domain.Entities;

public class InstructorAssignment : IAssignment<InstructorAssignment>
{
    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    
    private InstructorAssignment(Guid id, Guid resourceId)
    {
        Id = id;
        ResourceId = resourceId;
    }

    public static InstructorAssignment Create(Guid resourceId)
    {
        return new InstructorAssignment(Guid.NewGuid(), resourceId);
    }
}