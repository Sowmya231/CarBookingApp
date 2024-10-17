namespace CarBookingApp.ExceptionHandling
{
    public class InsufficientStockException:Exception
    {
        public InsufficientStockException() { }
        public InsufficientStockException(string message) : base(message) { }
    }
}
