using System.ComponentModel.DataAnnotations;

namespace GolfCourseManagement.DTOs
{
    public class CustomerDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
