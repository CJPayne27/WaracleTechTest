using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;
    private readonly ILogger<AvailabilityController> _logger;

    public AvailabilityController(IAvailabilityService availabilityService, ILogger<AvailabilityController> logger)
    {
        _availabilityService = availabilityService ?? throw new ArgumentNullException(nameof(availabilityService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets the available hotel rooms for the specified date range and number of guests.
    /// </summary>
    /// <param name="checkInDate">The check-in date.</param>
    /// <param name="checkOutDate">The check-out date.</param>
    /// <param name="numberOfGuests">The number of guests.</param>
    /// <returns>A list of available hotel rooms.</returns>
    [HttpGet("available-rooms")]
    public async Task<IActionResult> GetAvailableRooms(
        [FromQuery] DateTime checkInDate,
        [FromQuery] DateTime checkOutDate,
        [FromQuery] int numberOfGuests)
    {
        _logger.LogInformation("Availability: Received request to get available rooms");

        try
        {
            var result = await _availabilityService.GetAvailableRooms(checkInDate, checkOutDate, numberOfGuests);

            if (!result.Any())
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "No rooms available",
                    Detail = "No rooms are available for the specified dates and number of guests.",
                    Status = StatusCodes.Status404NotFound,
                    Instance = HttpContext.Request.Path
                };
                
                return NotFound(problemDetails);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred while creating the resource.",
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = HttpContext.Request.Path
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}