namespace Scheduling.Domain.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Task SaveAsync() => Task.CompletedTask;
}