namespace Core.Exceptions;

public class NoRightsException : Exception
{
    public NoRightsException() : base("Недостаточно прав!") { }
}