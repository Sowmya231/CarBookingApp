namespace CarBookingApp.ExceptionHandling
{
    public class UnAuthorizedAdminAccessException:Exception
    {
        public UnAuthorizedAdminAccessException()
        {

        }
        public UnAuthorizedAdminAccessException(string message) : base(message) { }
    }
}
