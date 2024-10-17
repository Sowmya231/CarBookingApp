namespace CarBookingApp.ExceptionHandling
{
    public class BookingCancellationException:Exception
    {
        public BookingCancellationException()
        {

        }
        public BookingCancellationException(string message) : base(message) { }
    }
}
