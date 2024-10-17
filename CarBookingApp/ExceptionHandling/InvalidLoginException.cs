namespace CarBookingApp.ExceptionHandling
{
    public class InvalidLoginException:Exception
    {
        public InvalidLoginException() { }
        public InvalidLoginException(string message) : base(message) { }
    }
}
