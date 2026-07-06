namespace Scheduling.Domain.Repositories;

// Business transactions as a single atomic operation.
// Implemented by infrastructure (e.g. over a DbContext).
public interface IUnitOfWork
{
    Task SaveAsync();
}