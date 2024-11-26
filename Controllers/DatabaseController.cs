using HotelWaracleBookingApi.Data.Seeding;
using HotelWaracleBookingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers;

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
            return Ok(
                new
                {
                    Message = "Database seeded successfully"
                });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    "Failed to seed database",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
        }
    }

    [HttpPost("Reset")]
    public async Task<IActionResult> ResetDatabase()
    {
        try
        {
            await _databaseSeeder.ResetMultipleAsync(typeof(Hotel), typeof(HotelRoom), typeof(BookingRequest));
            return Ok(
                new
                {
                    Message = "Database reset successfully"
                });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    "Failed to reset database",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
        }
    }

    private ProblemDetails CreateProblemDetails(string title, string detail, int statusCode)
    {
        return new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
            Instance = HttpContext.Request.Path
        };
    }
}