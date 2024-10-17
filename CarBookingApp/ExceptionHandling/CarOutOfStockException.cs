namespace CarBookingApp.ExceptionHandling
{
    public class CarOutOfStockException:Exception
    {
        public CarOutOfStockException()
        {

        }
        public CarOutOfStockException(string message) : base(message) { }
    }
}
