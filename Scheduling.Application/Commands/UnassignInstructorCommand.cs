namespace Scheduling.Application.Commands;

public record UnassignInstructorCommand(Guid TrainingEventId, Guid InstructorId);