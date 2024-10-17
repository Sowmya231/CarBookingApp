   using System;

namespace CarBookingApp.Models
{

    public class Notification
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
            public string UserId { get; set; } 
            public string Type { get; set; }   // E.g., "booking_confirmation", "reminder"
            public DateTime CreatedAt { get; set; } = DateTime.Now;
        }
    }


