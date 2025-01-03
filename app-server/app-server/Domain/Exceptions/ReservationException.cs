namespace app_server.Domain.Exceptions;

public class ReservationException : Exception
{

    public ReservationException() : base("reservation creation error") {}
    
    public ReservationException(string message) : base(message) {}
    
    public ReservationException(string message, Exception innerException) : base(message, innerException) {}

}