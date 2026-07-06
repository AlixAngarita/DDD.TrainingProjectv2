namespace Scheduling.Domain.Exceptions;

// Thrown when the target object does not exist
public class ObjectNotFoundException(string message) : Exception(message);
