using Scheduling.Domain.Exceptions;

namespace Scheduling.Domain.Values;

// Value object implementation of title name or name
public record Title
{
    public string Value { get; }
    private const int MaxLength = 255;
    
    private Title(string value) => Value = value;

    public static Title Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Title cannot be empty.");
        
        if  (value.Length > MaxLength)
            throw new DomainException($"Title cannot exceed {MaxLength} characters.");
        
        return new Title(value.Trim());
    }
}