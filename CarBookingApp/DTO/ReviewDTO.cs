namespace CarBookingApp.DTO
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }
        
        public int UserID { get; set; }
        

        public int CarID { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
