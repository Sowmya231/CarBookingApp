namespace CarBookingApp.ExceptionHandling
{
    public class BookingNotFoundException:Exception
    {
        public BookingNotFoundException() { }
        public BookingNotFoundException(string message) : base(message) { }
    }
}
