namespace Scheduling.Domain.Exceptions;

// Thrown when an operation would violate a business rule
public class DomainException(string message) : Exception(message);