using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCourseManagement.Models
{
    [Table("GolfCourses")]
    public class GolfCourse
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string CourseName { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;

        public int? NumHoles { get; set; }

        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<TeeTime>? TeeTimes { get; set; }
    }
}