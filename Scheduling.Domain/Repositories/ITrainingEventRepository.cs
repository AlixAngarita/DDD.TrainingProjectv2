using Scheduling.Domain.Entities;

namespace Scheduling.Domain.Repositories;

// Abstraction of the "memory" implementation for domain objects
public interface ITrainingEventRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task<TrainingEvent?> GetAsync(Guid id);
    void Add(TrainingEvent trainingEvent);
}