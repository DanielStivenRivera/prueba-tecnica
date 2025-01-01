namespace app_server.Domain.Exceptions;

public class UserAlreadyRegisteredException : Exception
{
    public UserAlreadyRegisteredException() : base("The user is already registered.") { }

    public UserAlreadyRegisteredException(string message) : base(message) { }

    public UserAlreadyRegisteredException(string message, Exception innerException) 
        : base(message, innerException) { }
}