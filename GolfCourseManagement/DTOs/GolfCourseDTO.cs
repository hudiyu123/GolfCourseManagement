using System.ComponentModel.DataAnnotations;

namespace GolfCourseManagement.DTOs
{
    public class GolfCourseDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string CourseName { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;

        public int? NumHoles { get; set; }

        public string? Description { get; set; }
    }
}
