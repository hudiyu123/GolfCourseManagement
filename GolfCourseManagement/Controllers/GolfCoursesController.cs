using GolfCourseManagement.DTOs;
using GolfCourseManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace GolfCourseManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GolfCoursesController : Controller
    {
        private readonly GolfCourseManagementDbContext _context;

        public GolfCoursesController(GolfCourseManagementDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetGolfCourses")]
        public async Task<ActionResult<List<GolfCourseDTO>>> GetGolfCourses()
        {
            var golfCourses = await _context.GolfCourses.Select(gc => new GolfCourseDTO()
            {
                ID = gc.ID,
                CourseName = gc.CourseName,
                Description = gc.Description,
                NumHoles = gc.NumHoles,
                Location = gc.Location,
            }).ToListAsync();
            return golfCourses;
        }

        [HttpGet("GetGolfCourse")]
        public async Task<ActionResult<GolfCourseDTO>> GetGolfCourse(int golfCourseID)
        {
            var golfCourse = await _context.GolfCourses.Where(gf => gf.ID == golfCourseID).FirstOrDefaultAsync();
            if (golfCourse != null)
            {
                return new GolfCourseDTO()
                {
                    ID = golfCourse.ID,
                    CourseName = golfCourse.CourseName,
                    Location = golfCourse.Location,
                    NumHoles = golfCourse.NumHoles,
                    Description = golfCourse.Description
                };
            }
            else
            {
                return BadRequest($"Cannot find golf course with id {golfCourseID}");
            }
        }

        [HttpPut("UpdateGolfCourse")]
        public async Task<ActionResult<GolfCourseDTO>> UpdateGolfCourse(GolfCourseDTO updatedGolfCourse)
        {
            var golfCourse = await _context.GolfCourses.Where(gf => gf.ID == updatedGolfCourse.ID).FirstOrDefaultAsync();
            if (golfCourse != null)
            {
                golfCourse.CourseName = updatedGolfCourse.CourseName;
                golfCourse.Location = updatedGolfCourse.Location;
                golfCourse.NumHoles = updatedGolfCourse.NumHoles;
                golfCourse.Description = updatedGolfCourse.Description;
                _context.GolfCourses.Update(golfCourse);
                await _context.SaveChangesAsync();
                return new GolfCourseDTO()
                {
                    ID = golfCourse.ID,
                    CourseName = golfCourse.CourseName,
                    Location = golfCourse.Location,
                    NumHoles = golfCourse.NumHoles,
                    Description = golfCourse.Description
                };
            }
            else
            {
                return BadRequest($"Cannot find golf course with id {updatedGolfCourse.ID}");
            }
        }

        [HttpPost("CreateGolfCourse")]
        public async Task<ActionResult<GolfCourseDTO>> CreateGolfCourse(GolfCourseDTO model)
        {
            var golfCourse = new GolfCourse()
            {
                CourseName = model.CourseName,
                Location = model.Location,
                NumHoles = model.NumHoles,
                Description = model.Description
            };
            _context.GolfCourses.Add(golfCourse);
            await _context.SaveChangesAsync();

            return new GolfCourseDTO()
            {
                ID = golfCourse.ID,
                CourseName = golfCourse.CourseName,
                Location = golfCourse.Location,
                NumHoles= golfCourse.NumHoles,
                Description = golfCourse.Description
            };
        }

        [HttpDelete("DeleteGolfCourse")]
        public async Task<ActionResult<GolfCourseDTO>> DeleteGolfCourse(int golfCourseID)
        {
            var golfCourse = await _context.GolfCourses
                .Where(gf => gf.ID == golfCourseID)
                .FirstOrDefaultAsync();
            if (golfCourse != null)
            {
                _context.GolfCourses.Remove(golfCourse);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest($"Cannot find golf course with id {golfCourseID}");
            }

            return new GolfCourseDTO()
            {
                ID = golfCourse.ID,
                CourseName = golfCourse.CourseName,
                Location = golfCourse.Location,
                NumHoles = golfCourse.NumHoles,
                Description = golfCourse.Description
            };
        }
    }
}
