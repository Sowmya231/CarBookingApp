using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Models
{
    /// <summary>
    /// Booking Properties 
    /// </summary>
        public class Booking
        {
            [Key]
            public int BookingID { get; set; } // Unique identifier for each booking

             
        [Required(ErrorMessage = "User ID is required.")]
            [ForeignKey("User")] // Specify that UserID is a foreign key to the User table
            public int UserID { get; set; } // Foreign key to the user who made the booking

            

            [Required(ErrorMessage = "Car ID is required.")]
            [ForeignKey("Car")] // Specify that CarID is a foreign key to the Car table
            public int CarID { get; set; } // Foreign key to the booked car

            [Required(ErrorMessage = "Booking date is required.")]
            public DateTime BookingDate { get; set; } // The date and time when the booking was made

            [Required(ErrorMessage = "Status is required.")]
            [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
            public string Status { get; set; } = "Confirmed"; // Booking status (default is "Confirmed")

            [Required(ErrorMessage = "Source is required.")]
            [StringLength(100, ErrorMessage = "Source cannot exceed 100 characters.")]
            public string Source { get; set; } // Starting point of the trip

            [Required(ErrorMessage = "Destination is required.")]
            [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters.")]
            public string Destination { get; set; } // Ending point of the trip

            public double Distance { get; set; } // Distance between source and destination in kilometers

            public decimal Price { get; set; } // Total price of the booking

        }
    }



