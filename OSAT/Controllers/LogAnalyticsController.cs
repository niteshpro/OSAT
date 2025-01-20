using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSAT.Models;

namespace OSAT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogAnalyticsController : ControllerBase
    {
        private readonly LogDbContext _context;

        public LogAnalyticsController(LogDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddLog([FromBody] Log log)
        {
            if (log == null)
            {
                return BadRequest("Log data is missing.");
            }

            try
            {
                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
                return Ok("Log added to the database successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getstats")]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                // Fetch the top 10 recent logs, ordered by Timestamp descending
                var recentLogs = await _context.Logs
                    .OrderByDescending(log => log.Timestamp) // Sort by Timestamp descending
                    .Take(10)                                // Take the top 10 records
                    .ToListAsync();

                return Ok(recentLogs); // Return the logs as a JSON response
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching stats.", error = ex.Message });
            }
        }

    }

}
