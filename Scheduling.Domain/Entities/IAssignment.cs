namespace Scheduling.Domain.Entities;

public interface IAssignment<T>
{
    Guid Id { get; set; }
    Guid ResourceId { get; set; }

    static abstract T Create(Guid resourceId);
}