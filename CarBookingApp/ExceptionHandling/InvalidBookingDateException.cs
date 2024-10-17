namespace CarBookingApp.ExceptionHandling
{
    public class InvalidBookingDateException:Exception
    {
        public InvalidBookingDateException()
        {

        }
        public InvalidBookingDateException(string message) : base(message) { }
    }
}
