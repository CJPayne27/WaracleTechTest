using HotelWaracleBookingApi.Data;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly HotelWaracleDbContext _context;
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(HotelWaracleDbContext context, IBookingService bookingService, ILogger<BookingController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves a booking by its Id.
    /// </summary>
    /// <param name="bookingId">The Id of the Booking</param>
    /// <returns>A valid booking</returns>
    [HttpGet("{bookingId:Guid}")]
    public async Task<IActionResult?> GetBookingById(Guid bookingId)
    {
        try
        {
            _logger.LogInformation("Bookings: Received request to get booking by ID: {BookingId}", bookingId);

            var booking = await _bookingService.GetBookingById(bookingId);

            if (booking == null)
            {
                _logger.LogWarning("Bookings: No booking found for ID: {BookingId}", bookingId);
                return NotFound("No booking found.");
            }

            return Ok(booking);
        }
        catch (Exception ex)
        {
            return HandleInternalError(ex, $"Error occurred while retrieving booking with ID: {bookingId}.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking(Booking booking)
    {
        try
        {
            //TODO: Check booking validity
            //TODO: Check booking dates
            //TODO: Check room availability
            //TODO: Set booking status

            return Ok(new Booking());
        }
        catch (Exception e)
        {
            return HandleInternalError(e, "There has been an error while booking the hotel room");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        var bookings = await _context.Bookings.ToListAsync();
        return Ok(bookings);
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