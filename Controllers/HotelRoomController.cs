using HotelWaracleBookingApi.Models;
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
                return NotFound(
                    CreateProblemDetails(
                        "No hotel rooms found.",
                        "No hotel rooms found.",
                        StatusCodes.Status404NotFound));
            }

            return Ok(hotelRooms);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    "Error occurred while retrieving all hotel rooms.",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
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

            return BadRequest(
                CreateProblemDetails(
                    "Hotel ID is invalid",
                    "Hotel ID cannot be null or empty.",
                    StatusCodes.Status400BadRequest));
        }

        try
        {
            var hotelRooms = await _hotelRoomService.GetHotelRoomsByHotelId(hotelId);

            if (!hotelRooms.Any())
            {
                _logger.LogWarning("HotelRooms: No rooms found for HotelId: {HotelId}", hotelId);

                return BadRequest(
                    CreateProblemDetails(
                        "No hotel rooms found",
                        $"No rooms found for hotel ID: {hotelId}",
                        StatusCodes.Status400BadRequest));
            }

            return Ok(hotelRooms);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    $"Error occurred while retrieving rooms for HotelId: {hotelId}.",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
        }
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateHotelRoom(Guid id, HotelRoom hotelRoom)
    {
        var existingHotelRoom = await _hotelRoomService.GetHotelRoomByRoomId(hotelRoom.Id, hotelRoom.HotelId);

        if (existingHotelRoom == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Hotel Room Not Found",
                Detail = $"No hotel room found with ID {id}",
                Status = StatusCodes.Status404NotFound
            });
        }

        hotelRoom.IsOccupied = true;

        await _hotelRoomService.UpdateHotelRoom(hotelRoom);

        return Ok(new { Message = "Hotel room updated successfully" });
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