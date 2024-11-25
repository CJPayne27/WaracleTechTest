using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelRoomController : ControllerBase
{
    private readonly IHotelRoomService _hotelRoomService;
    private readonly ILogger<HotelRoomController> _logger;

    public HotelRoomController(IHotelRoomService hotelRoomService, ILogger<HotelRoomController> logger)
    {
        _hotelRoomService = hotelRoomService ?? throw new ArgumentNullException(nameof(hotelRoomService));
        _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<HotelRoomController>));
    }

    /// <summary>
    /// Retrieves all hotel rooms
    /// </summary>
    /// <returns>List of all valid hotel rooms.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllHotelRooms()
    {
        try
        {
            var hotelRooms = await _hotelRoomService.GetAllHotelRooms();

            if (!hotelRooms.Any())
            {
                return NotFound("No hotel rooms found.");
            }

            return Ok(hotelRooms);
        }
        catch (Exception e)
        {
            return HandleInternalError(e, "Error occurred while retrieving all hotel rooms.");
        }
    }

    /// <summary>
    /// Retrieves all hotel rooms by hotel ID
    /// </summary>
    /// <param name="hotelId">The Id of the Hotel</param>
    /// <returns>List of valid hotel rooms for that specific hotel, or a 404 if no hotel rooms found.</returns>
    [HttpGet("{hotelId}")]
    public async Task<IActionResult> GetHotelRoomsByHotelId(string hotelId)
    {
        if (string.IsNullOrWhiteSpace(hotelId))
        {
            _logger.LogWarning("HotelRooms: Invalid HotelId: {HotelId} provided", hotelId);

            return BadRequest("Hotel ID cannot be null or empty.");
        }

        try
        {
            var hotelRooms = await _hotelRoomService.GetHotelRoomsByHotelId(hotelId);

            if (!hotelRooms.Any())
            {
                _logger.LogWarning("HotelRooms: No rooms found for HotelId: {HotelId}", hotelId);

                return NotFound($"No rooms found for hotel ID: {hotelId}");
            }

            return Ok(hotelRooms);
        }
        catch (Exception e)
        {
            return HandleInternalError(e, $"Error occurred while retrieving rooms for HotelId: {hotelId}.");
        }
    }

    /// <summary>
    /// Handles internal server errors consistently and logs the exception.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="contextMessage">Optional context-specific message for logging.</param>
    private IActionResult HandleInternalError(Exception exception, string contextMessage = null)
    {
        if (!string.IsNullOrWhiteSpace(contextMessage))
        {
            _logger.LogError(exception, contextMessage);
        }
        else
        {
            _logger.LogError(exception, "An unexpected error occurred.");
        }

        return StatusCode(500, "An error occurred while processing your request. Please try again later.");
    }
}