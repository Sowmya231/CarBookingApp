using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Models
{

    /// <summary>
    /// Car Properties 
    /// </summary>
    public class Car
    {
        [Key]
        public int CarID { get; set; } 

        [Required(ErrorMessage = "Car name is required.")]
        [StringLength(100, ErrorMessage = "Car name cannot exceed 100 characters.")]
        public string CarName { get; set; } 

        //[Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } 

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; } 

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } 

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; } 

        public string ImageURL { get; set; } 

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; } 
        public List<Review> Reviews { get; set; }

        
    }

}
