using Scheduling.Domain.Exceptions;

namespace Scheduling.Domain.Values;

// Time interval with a start and end date.
// Validates that the end occurs after the start and that
// the maximum period is 1 year (business rule)

public record Period
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    private static readonly TimeSpan MaxDuration = TimeSpan.FromDays(366);
    
    private Period(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
    
    public static Period Of(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
            throw new DomainException("A period must end after it starts.");

        if (endDate - startDate > MaxDuration)
            throw new DomainException("A period cannot be longer than one year.");
        
        return new Period(startDate, endDate);
    }
}