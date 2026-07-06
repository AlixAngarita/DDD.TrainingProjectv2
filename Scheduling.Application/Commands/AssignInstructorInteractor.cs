using Scheduling.Domain.Exceptions;
using Scheduling.Domain.Repositories;

namespace Scheduling.Application.Commands;

public class AssignInstructorInteractor
{
    private readonly ITrainingEventRepository Repository;

    public AssignInstructorInteractor(ITrainingEventRepository repository) => Repository = repository;

    public async Task<Guid> Handle(AssignInstructorCommand command)
    {
        // 1. Load the aggregate 
        var trainingEvent = await Repository.GetAsync(command.TrainingEventId)
                            ?? throw new ObjectNotFoundException($"Training event '{command.TrainingEventId}' was not found.");
        
        // 2. Invoke behavior from domain but first the application layer is responsible for finding objects
        var assignment = trainingEvent.AssignInstructor(command.AssigneeId);

        // 3. Commit the transaction.
        await Repository.UnitOfWork.SaveAsync(); // Not implemented

        return assignment.Id;
    }
}