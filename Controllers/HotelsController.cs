using HotelWaracleBookingApi.Data;
using HotelWaracleBookingApi.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return Ok(hotels);
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotels), new { id = hotel.Id }, hotel);
        }
    }
}
