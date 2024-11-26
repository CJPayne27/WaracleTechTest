using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelWaracleBookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHotelRoomRepository _hotelRoomRepository;
    private readonly IHotelRoomService _hotelRoomService;
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingRepository bookingRepository, IHotelRoomRepository hotelRoomRepository, IBookingService bookingService, ILogger<BookingController> logger, IHotelRoomService hotelRoomService)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _hotelRoomRepository = hotelRoomRepository ?? throw new ArgumentNullException(nameof(hotelRoomRepository));
        _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _hotelRoomService = hotelRoomService ?? throw new ArgumentNullException(nameof(hotelRoomService));
    }

    /// <summary>
    /// Retrieves a booking by its id.
    /// </summary>
    /// <param name="bookingId">The id of the BookingRequest</param>
    /// <returns>A valid booking</returns>
    [HttpGet("{bookingId:Guid}")]
    public async Task<IActionResult?> GetBookingById(Guid bookingId)
    {
        _logger.LogInformation("Bookings: Received request to get booking by ID: {BookingId}", bookingId);

        try
        {
            var booking = await _bookingService.GetBookingById(bookingId);
            if (booking == null)
            {
                return NotFound(
                    CreateProblemDetails(
                        "No booking found",
                        "No booking found for the specified ID.",
                        StatusCodes.Status404NotFound));
            }

            return Ok(booking);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving booking with ID: {BookingId}", bookingId);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                    CreateProblemDetails(
                        "Error occurred",
                        ex.Message,
                        StatusCodes.Status500InternalServerError));
        }
    }

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="bookingRequest">Check-In date, Check-Out date, Number of Guests</param>
    /// <returns>Id of a successful booking.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateBooking(BookingRequest bookingRequest)
    {
        var response = new BookingResponse();

        try
        {
            var validationError = ValidateBookingRequest(bookingRequest);
            if (validationError != null)
            {
                return BadRequest(
                    CreateProblemDetails(
                        validationError.Title!,
                        validationError.Detail!,
                        StatusCodes.Status400BadRequest));
            }

            var room = await _hotelRoomService.GetHotelRoomByRoomId(bookingRequest.HotelRoomId, bookingRequest.HotelId);
            
            if (room == null || room.IsOccupied)
            {
                return BadRequest(
                    CreateProblemDetails(
                    "Room unavailable",
                    "The selected room does not exist or is unavailable.",
                    StatusCodes.Status400BadRequest));
            }

            if (room.MinCapacity > bookingRequest.NumberOfGuests ||
                room.MaxCapacity < bookingRequest.NumberOfGuests)
            {
                return BadRequest(
                    CreateProblemDetails(
                        "Capacity issue",
                        $"The room cannot accommodate {bookingRequest.NumberOfGuests} guests.",
                        StatusCodes.Status400BadRequest));
            }

            var overlappingBookings = await _bookingService.GetAllBookingsBetweenDateRange(bookingRequest.CheckInDate, bookingRequest.CheckOutDate);
            {
                if (overlappingBookings.Any(b => b.HotelRoomId == bookingRequest.HotelRoomId))
                {
                    return BadRequest(CreateProblemDetails(
                        "Room not available", 
                        "The selected room is not available for the chosen dates.", 
                        StatusCodes.Status400BadRequest));
                }
            }

            bookingRequest.Status = "Confirmed";
            bookingRequest.BookingDate = DateTime.UtcNow;

            var createdBooking = await _bookingRepository.CreateBooking(bookingRequest);

            response.Id = createdBooking.Id;
            response.Message = "Booking created successfully.";

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a booking.");

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

    private ProblemDetails? ValidateBookingRequest(BookingRequest request)
    {
        if (request.CheckInDate == request.CheckOutDate)
        {
            return CreateProblemDetails(
                "Invalid stay duration",
                "Minimum of 1 night stay required for BookingRequest",
                StatusCodes.Status400BadRequest);
        }

        if (request.CheckInDate < DateTime.UtcNow)
        {
            return CreateProblemDetails(
                "Invalid check-in date",
                "BookingRequest cannot be made for past dates.",
                StatusCodes.Status400BadRequest);
        }

        return null;
    }
}