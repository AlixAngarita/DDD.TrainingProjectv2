namespace Scheduling.Domain.Entities;

public class InstructorAssignment : Assignment
{
    private InstructorAssignment(Guid id, Guid resourceId) : base(id, resourceId) { }

    public static InstructorAssignment Create(Guid assigneeId)
    {
        return new InstructorAssignment(Guid.NewGuid(), assigneeId);
    }
}