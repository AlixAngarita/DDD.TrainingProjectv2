namespace Scheduling.Application.Commands;

public record AssignInstructorCommand(Guid TrainingEventId, Guid AssigneeId);