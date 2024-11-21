using HotelWaracleBookingApi.Data.Seeding;
using HotelWaracleBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseSeeder _databaseSeeder;

        public DatabaseController(DatabaseSeeder databaseSeeder)
        {
            _databaseSeeder = databaseSeeder ?? throw new ArgumentNullException(nameof(databaseSeeder));
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            try
            {
                await _databaseSeeder.SeedAsync();
                return Ok(new { Message = "Database seeded successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to seed database", Details = ex.Message });
            }
        }

        [HttpPost("Reset")]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                await _databaseSeeder.ResetMultipleAsync(typeof(Hotel), typeof(HotelRoom), typeof(Booking));
                return Ok(new { Message = "Database reset successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Failed to reset database", Details = ex.Message });
            }
        }
    }
}
