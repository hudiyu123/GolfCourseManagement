using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCourseManagement.Models
{
    [Table("TeeTimes")]
    public class TeeTime
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [ForeignKey("GolfCourse")]
        [Required]
        public int GolfCourseID { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MaxPlayersPerSlot { get; set; }

        public int Occupancy { get; set; }

        // Navigation properties
        public virtual GolfCourse? GolfCourse { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
