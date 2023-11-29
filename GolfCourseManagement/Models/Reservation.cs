using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GolfCourseManagement.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("TeeTime")]
        [Required]
        public int TeeTimeID { get; set; }

        [Required]
        public DateTime ReservationDateTime { get; set; }

        [Required]
        public int NumPlayers { get; set; }

        // Navigation properties
        public virtual Customer? Customer { get; set; }

        public virtual TeeTime? TeeTime { get; set; }
    }
}
