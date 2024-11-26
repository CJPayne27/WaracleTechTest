using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly ILogger<HotelController> _logger;

    public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
    {
        _hotelService = hotelService ?? throw new ArgumentNullException(nameof(hotelService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves all hotels
    /// </summary>
    /// <returns>List of all valid hotels.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllHotels()
    {
        try
        {
            _logger.LogInformation("Hotels: Received request to get all hotels");
            var hotels = await _hotelService.GetAllHotels();

            if (!hotels.Any())
            {
                var message = "No hotels found.";

                _logger.LogInformation($"Hotels: {message}");

                return NotFound(
                    CreateProblemDetails(
                        message,
                        message,
                        StatusCodes.Status404NotFound));
            }

            return Ok(hotels);
        }
        catch (Exception ex)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    "Error occurred",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
        }
    }

    /// <summary>
    /// Retrieves a hotel by ID
    /// </summary>
    /// <param name="hotelId">The Id of the Hotel.</param>
    /// <returns>List of valid hotels, or a 404 if no hotels found.</returns>
    [HttpGet("{hotelId}")]
    public async Task<IActionResult> GetHotelByHotelId(string hotelId)
    {
        if (string.IsNullOrEmpty(hotelId))
        {
            _logger.LogWarning("Hotels: {HotelId} is invalid.", hotelId);

            return BadRequest(
                CreateProblemDetails(
                    "HotelId invalid",
                    "The HotelId should not be null or empty",
                    StatusCodes.Status400BadRequest));
        }

        try
        {
            var hotel = await _hotelService.GetHotelByHotelId(hotelId);

            if (hotel == null)
            {
                _logger.LogInformation("Hotels: No hotels found for HotelId: {HotelId}", hotelId);

                return NotFound(
                    CreateProblemDetails(
                        "No hotels found",
                        $"No hotels found for hotel ID: {hotelId}",
                        StatusCodes.Status404NotFound));
            }

            return Ok(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hotels: An error occurred while processing the GetHotelByHotelId request");

            return StatusCode(StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    "Error occurred",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
        }
    }

    /// <summary>
    /// Retrieves a hotel by name
    /// </summary>
    /// <param name="name">Name of the Hotel.</param>
    /// <returns>Hotel details, or a 404 if no hotel is found.</returns>
    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetHotelByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Hotels: Invalid Hotel name: {name} provided", name);

            return BadRequest("Hotel name cannot be null or empty.");
        }

        try
        {
            var hotel = await _hotelService.GetHotelByName(name);
            if (hotel == null)
            {
                return NotFound(
                    CreateProblemDetails(
                "No hotels found",
                        $"No hotels found for hotel name: {name}",
                        StatusCodes.Status404NotFound));
            }

            return Ok(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hotels: An error occurred while processing the GetHotelByName request");

            return StatusCode(StatusCodes.Status500InternalServerError,
                CreateProblemDetails(
                    "Error occurred",
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