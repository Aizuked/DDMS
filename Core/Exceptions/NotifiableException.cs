namespace Core.Exceptions;

public class NotifiableException : Exception
{
    public NotifiableException() { }

    public NotifiableException(string? message)
        : base(message) { }

    public NotifiableException(string? message, Exception? innerException)
        : base(message, innerException) { }
}