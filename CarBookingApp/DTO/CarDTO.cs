
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    //using Car_booking_app.Model;

namespace CarBookingApp.DTO
{
    public class CarDTO
        {
            public int CarID { get; set; } // Unique identifier for each car

            [Required(ErrorMessage = "Car name is required.")]
            [StringLength(100, ErrorMessage = "Car name cannot exceed 100 characters.")]
            public string CarName { get; set; } // Name of the car

            public string Description { get; set; } // Description of the car (e.g., features, model, etc.)

            [Required(ErrorMessage = "Price is required.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
            public decimal Price { get; set; } // Rental price of the car

            [Required(ErrorMessage = "Category is required.")]
            public string Category { get; set; } // Category of the car (e.g., SUV, Sedan, etc.)

            [Required(ErrorMessage = "Stock quantity is required.")]
            [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
            public int StockQuantity { get; set; } // Number of cars available

            //public string Brand { get; set; } // Brand of the car

            public string ImageURL { get; set; } // URL of the car's image

            //public List<Review>? Reviews { get; set; } // List of reviews for the car

            [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
            public double Rating { get; set; } // Average rating of the car


        }
    }


