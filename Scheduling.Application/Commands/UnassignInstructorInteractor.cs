using Scheduling.Domain.Exceptions;
using Scheduling.Domain.Repositories;

namespace Scheduling.Application.Commands;

public class UnassignInstructorInteractor
{
    private readonly ITrainingEventRepository Repository;

    public UnassignInstructorInteractor(ITrainingEventRepository repository) => Repository = repository;

    public async Task Handle(UnassignInstructorCommand command)
    {
        // 1. Load the aggregate 
        var trainingEvent = await Repository.GetAsync(command.TrainingEventId)
                            ?? throw new ObjectNotFoundException($"Training event '{command.TrainingEventId}' was not found.");
        
        // 2. Invoke behavior from domain but first the application layer is responsible for finding objects
        var assignment = trainingEvent.InstructorAssignments.FirstOrDefault(a => a.Id == command.InstructorId)
                         ?? throw new ObjectNotFoundException($"Instructor '{command.InstructorId}' was not found.");
        trainingEvent.UnassignInstructor(assignment);

        // 3. Commit the transaction.
        await Repository.UnitOfWork.SaveAsync(); // Not implemented
    }
}