using CarBookingApp.Models;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.DTO
{
   public class BookingDTO
        {
            [Key]
             public int BookingID { get; set; }
             //public virtual UserManagement? UserManagement { get; set; }

            [Required(ErrorMessage = "UserID is required.")]
            public int UserID { get; set; } // ID of the user making the booking

            [Required(ErrorMessage = "CarID is required.")]
            public int CarID { get; set; } // ID of the car being booked

            [Required(ErrorMessage = "Source location is required.")]
            [StringLength(100, ErrorMessage = "Source location cannot exceed 100 characters.")]
            public string Source { get; set; } // Starting point of the trip

            [Required(ErrorMessage = "Destination location is required.")]
            [StringLength(100, ErrorMessage = "Destination location cannot exceed 100 characters.")]
            public string Destination { get; set; } // Ending point of the trip

            [Required(ErrorMessage = "Booking date is required.")]
            public DateTime BookingDate { get; set; } = DateTime.Now; // Default booking date

            [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
            public string Status { get; set; } = "Confirmed"; // Default status is "Confirmed"
        }
    }


