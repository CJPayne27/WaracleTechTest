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

            if (hotels == null || !hotels.Any())
            {
                _logger.LogInformation("Hotels: No hotels found.");
                return NotFound("No hotels found.");
            }

            return Ok(hotels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hotels: An error occurred while retrieving all hotels");
            return HandleInternalError();
        }
    }

    /// <summary>
    /// Retrieves a hotel by ID
    /// </summary>
    /// <param name="hotelId">The Id of the Hotel.</param>
    /// <returns>List of valid hotels, or a 404 if no hotels found.</returns>
    [HttpGet("{hotelId:int}")]
    public async Task<IActionResult> GetHotelByHotelId(int hotelId)
    {
        if (hotelId <= 0) // Validity of min/max hotelId
        {
            _logger.LogWarning("Hotels: {HotelId} is invalid.", hotelId);

            return BadRequest("Hotel Id cannot be less than or 0.");
        }

        try
        {
            var hotel = await _hotelService.GetHotelByHotelId(hotelId);

            if (hotel == null)
            {
                _logger.LogInformation("Hotels: No hotels found for HotelId: {HotelId}", hotelId);

                return NotFound($"No hotels found for hotel ID: {hotelId}");
            }

            return Ok(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hotels: An error occurred while processing the GetHotelByHotelId request");

            return HandleInternalError();
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
                return NotFound($"No hotels found for hotel name: {name}");
            }

            return Ok(hotel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hotels: An error occurred while processing the GetHotelByName request");

            return HandleInternalError();
        }
    }

    //[HttpPost("/create")]
    //public async Task<IActionResult> CreateHotel(Hotel hotel)
    //{
    //    _context.Hotels.Add(hotel);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction(nameof(GetHotels), new { id = hotel.Id }, hotel);
    //}

    /// <summary>
    /// Handles internal server errors consistently.
    /// </summary>
    private IActionResult HandleInternalError()
    {
        return StatusCode(500, "An error occurred while processing your request. Please try again later.");
    }
}