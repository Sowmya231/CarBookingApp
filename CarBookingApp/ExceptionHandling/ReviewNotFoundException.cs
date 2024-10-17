namespace CarBookingApp.ExceptionHandling
{
    public class ReviewNotFoundException:Exception
    {
        public ReviewNotFoundException()
        {

        }
        public ReviewNotFoundException(string message) : base(message) { }
    }
}
