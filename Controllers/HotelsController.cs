using HotelWaracleBookingApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly HotelWaracleDbContext _context;
        private readonly ILogger<HotelsController> _logger;

        public HotelsController(HotelWaracleDbContext context, ILogger<HotelsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all hotels
        /// </summary>
        /// <returns>List of all valid hotels.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            _logger.LogInformation("Hotels: Received request to get all hotels");

            try
            {
                var hotels = await _context.Hotels.ToListAsync();

                _logger.LogInformation("Hotels: Processing request to return all hotels");

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hotels: An error occurred while processing the GetAllHotelRooms request");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a hotel by ID
        /// </summary>
        /// <param name="hotelId">The Id of the Hotel.</param>
        /// <returns>List of valid hotels, or a 404 if no hotels found.</returns>
        [HttpGet("/{hotelId:int}")]
        public async Task<IActionResult> GetHotelByHotelId(int hotelId)
        {
            _logger.LogInformation("Hotels: Received request to get hotel information for HotelId: {Id}", hotelId);

            if (hotelId <= 0) // Validity of min/max hotelId
            {
                _logger.LogWarning("Hotels: {HotelId} is invalid.", hotelId);

                return BadRequest("Hotel Id cannot be less than or 0.");
            }

            try
            {
                var hotel = await _context.Hotels.FindAsync(hotelId);

                if (hotel == null)
                {
                    _logger.LogInformation("Hotels: No hotels found for HotelId: {HotelId}", hotelId);

                    return NotFound($"No hotels found for hotel ID: {hotelId}");
                }

                _logger.LogInformation("Hotels: Processing request to return hotel rooms for HotelId: {HotelId}", hotelId);

                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hotels: An error occurred while processing the GetHotelByHotelId request");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a hotel by name
        /// </summary>
        /// <param name="name">Name of the Hotel.</param>
        /// <returns>A single entity of Hotel</returns>
        [HttpGet("/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            _logger.LogInformation("Hotels: Received request to get hotel information for Hotel: {name}", name);

            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Hotels: Invalid Hotel name: {name} provided", name);

                return BadRequest("Hotel name cannot be null or empty.");
            }

            try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Name == name);
                if (hotel == null)
                {
                    return NotFound($"No hotels found for hotel name: {name}");
                }

                _logger.LogInformation("Hotels: Processing request to return hotel details for Hotel name: {name}", name);

                return Ok(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hotels: An error occurred while processing the GetHotelByName request");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //[HttpPost("/create")]
        //public async Task<IActionResult> CreateHotel(Hotel hotel)
        //{
        //    _context.Hotels.Add(hotel);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetHotels), new { id = hotel.Id }, hotel);
        //}
    }
}
