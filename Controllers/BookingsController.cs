using HotelWaracleBookingApi.Data;
using HotelWaracleBookingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly HotelWaracleDbContext _context;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(HotelWaracleDbContext context, ILogger<BookingsController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(Booking booking)
    {
        var room = await _context.HotelRooms.FindAsync(booking.HotelRoomId);
        if (room == null) return NotFound("Room not found.");

        if (booking.NumberOfGuests < room.MinCapacity || booking.NumberOfGuests > room.MaxCapacity)
            return BadRequest("Invalid number of guests for the selected room.");

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, booking);
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        var bookings = await _context.Bookings.ToListAsync();
        return Ok(bookings);
    }
}