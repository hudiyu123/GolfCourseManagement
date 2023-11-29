using GolfCourseManagement.DTOs;
using GolfCourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : Controller
    {
        private readonly GolfCourseManagementDbContext _context;
        public ReservationsController(GolfCourseManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetReservations")]
        public async Task<ActionResult<List<ReservationDTO>>> GetReservations(int customerID)
        {
            var reservations = await _context.Reservations
                .Where(r => r.CustomerID == customerID)
                .OrderBy(r => r.ReservationDateTime)
                .Select(r => new ReservationDTO()
                {
                    ID = r.ID,
                    CustomerID = r.CustomerID,
                    TeeTimeID = r.TeeTimeID,
                    ReservationDateTime = r.ReservationDateTime,
                    NumPlayers = r.NumPlayers
                }).ToListAsync();
            return reservations;
        }

        [HttpPost("CreateReservation")]
        public async Task<ActionResult<ReservationDTO>> CreateReservation(int customerID, int teeTimeID, int numPlayers)
        {
            var teeTime = await _context.TeeTimes.Where(tt => tt.ID == teeTimeID).FirstOrDefaultAsync();
            if (teeTime != null)
            {
                if (teeTime.Occupancy >= numPlayers)
                {
                    teeTime.Occupancy -= numPlayers;
                }
                else
                {
                    return BadRequest("Tee time {teeTimeID} doesn't have enough occupancy");
                }
            }
            else
            {
                return BadRequest("Cannot fing tee time with ID {teeTimeID}");
            }

            var reservation = new Reservation()
            {
                CustomerID = customerID,
                TeeTimeID = teeTimeID,
                NumPlayers = numPlayers,
                ReservationDateTime = DateTime.Now
            };
            _context.TeeTimes.Update(teeTime);
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new ReservationDTO()
            {
                ID = reservation.ID,
                CustomerID = reservation.CustomerID,
                TeeTimeID = reservation.TeeTimeID,
                ReservationDateTime = reservation.ReservationDateTime,
                NumPlayers = reservation.NumPlayers
            };
        }

        [HttpDelete("DeleteReservation")]
        public async Task<ActionResult<ReservationDTO>> DeleteReservation(int reservationID)
        {
            var reservation = await _context.Reservations.Where(r => r.ID == reservationID).FirstOrDefaultAsync();
            if (reservation != null)
            {
                var teeTime = await _context.TeeTimes.Where(tt => tt.ID == reservation.TeeTimeID).FirstOrDefaultAsync();
                if (teeTime != null)
                {
                    teeTime.Occupancy += reservation.NumPlayers;
                    _context.TeeTimes.Update(teeTime);
                    _context.Reservations.Remove(reservation);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Cannot find tee time with ID {reservationID} in reservation {reservationID}");
                }
            }
            else
            {
                return BadRequest("Cannot find reservation with ID {reservationID}");
            }

            return new ReservationDTO()
            {
                ID = reservation.ID,
                CustomerID = reservation.CustomerID,
                TeeTimeID = reservation.TeeTimeID,
                ReservationDateTime = reservation.ReservationDateTime,
                NumPlayers = reservation.NumPlayers
            };
        }

    }
}
