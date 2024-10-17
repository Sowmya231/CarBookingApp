using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "MinimunLength should be 8 and Maxlength should be 15")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
