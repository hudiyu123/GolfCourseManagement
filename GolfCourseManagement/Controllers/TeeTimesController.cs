using GolfCourseManagement.DTOs;
using GolfCourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GolfCourseManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeeTimesController : Controller
    {
        private readonly GolfCourseManagementDbContext _context;
        public TeeTimesController(GolfCourseManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetTeeTimes")]
        public async Task<ActionResult<List<TeeTimeDTO>>> GetTeeTimes(int golfCourseID)
        {
            var teeTimes = await _context.TeeTimes
                .Where(tt => tt.GolfCourseID == golfCourseID)
                .OrderBy(tt => tt.StartTime)
                .Select(tt => new TeeTimeDTO()
                {
                    ID = tt.ID,
                    GolfCourseID = tt.GolfCourseID,
                    StartTime = tt.StartTime,
                    EndTime = tt.EndTime,
                    MaxPlayersPerSlot = tt.MaxPlayersPerSlot,
                    Occupancy = tt.Occupancy
                }).ToListAsync();
            return teeTimes;
        }

        [HttpPost("CreateTeeTime")]
        public async Task<ActionResult<TeeTimeDTO>> CreateTeeTime(TeeTimeDTO model)
        {
            var teeTime = new TeeTime()
            {
                GolfCourseID = model.GolfCourseID,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                MaxPlayersPerSlot = model.MaxPlayersPerSlot,
                Occupancy = model.MaxPlayersPerSlot
            };
            _context.TeeTimes.Add(teeTime);
            await _context.SaveChangesAsync();

            return new TeeTimeDTO()
            {
                ID = teeTime.ID,
                GolfCourseID = teeTime.GolfCourseID,
                StartTime = teeTime.StartTime,
                EndTime = teeTime.EndTime,
                MaxPlayersPerSlot = teeTime.MaxPlayersPerSlot,
                Occupancy = teeTime.Occupancy
            };
        }

        [HttpDelete("DeleteTeeTime")]
        public async Task<ActionResult<TeeTimeDTO>> DeleteTeeTime(int teeTimeID)
        {
            var teeTime = await _context.TeeTimes
                .Where(tt => tt.ID == teeTimeID)
                .FirstOrDefaultAsync();
            if (teeTime != null)
            {
                _context.TeeTimes.Remove(teeTime);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest($"Cannot find tee time with id {teeTimeID}");
            }

            return new TeeTimeDTO()
            {
                ID = teeTime.ID,
                GolfCourseID = teeTime.GolfCourseID,
                StartTime = teeTime.StartTime,
                EndTime = teeTime.EndTime,
                MaxPlayersPerSlot = teeTime.MaxPlayersPerSlot,
                Occupancy = teeTime.Occupancy
            };
        }
    }
}
