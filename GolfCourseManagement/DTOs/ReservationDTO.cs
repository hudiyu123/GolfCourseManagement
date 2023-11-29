using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GolfCourseManagement.DTOs
{
    public class ReservationDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int TeeTimeID { get; set; }

        public DateTime? ReservationDateTime { get; set; }

        [Required]
        public int NumPlayers { get; set; }
    }
}
