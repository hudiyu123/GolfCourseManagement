using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GolfCourseManagement.DTOs
{
    public class TeeTimeDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int GolfCourseID { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MaxPlayersPerSlot { get; set; }

        public int Occupancy { get; set; }
    }
}
