namespace CarBookingApp.ExceptionHandling
{
    public class CarNotFoundException:Exception
    {
        public CarNotFoundException() { }
        public CarNotFoundException(string message) : base(message) { }
    }
}
