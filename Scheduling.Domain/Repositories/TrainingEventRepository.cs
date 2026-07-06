using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

public class TrainingEventRepository : ITrainingEventRepository
{
    private readonly Dictionary<Guid, TrainingEvent> store = new(); // simulation of a DbContext
    public IUnitOfWork UnitOfWork { get; } = new UnitOfWork(); // should actually point to the dbContext
    
    public Task<TrainingEvent?> GetAsync(Guid trainingEventId)
    {
        store.TryGetValue(trainingEventId, out var trainingEvent);
        return Task.FromResult<TrainingEvent?>(trainingEvent);
    }

    public void Add(TrainingEvent trainingEvent)
    {
        store.Add(trainingEvent.Id, trainingEvent);
    }
}