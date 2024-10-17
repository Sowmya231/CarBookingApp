using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Models
{

    /// <summary>
    /// Review Properties 
    /// </summary>
    public class Review
    {
        [Key]
        public int ReviewID { get; set; } 

       
        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey("User")]
        public int UserID { get; set; } 

         
        [Required(ErrorMessage = "Car ID is required.")]
        [ForeignKey("Car")]
        public int CarID { get; set; } 

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; } 

        public string Comment { get; set; } 

        [Required(ErrorMessage = "Review date is required.")]
        public DateTime ReviewDate { get; set; } 



    }

}
