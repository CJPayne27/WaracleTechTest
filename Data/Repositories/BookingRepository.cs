using HotelWaracleBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Data.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly HotelWaracleDbContext _context;

    public BookingRepository(HotelWaracleDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<BookingRequest?> GetBookingById(Guid bookingId)
    {
        return await _context.Bookings.FindAsync(bookingId);
    }

    public async Task<IEnumerable<BookingRequest?>> GetAllBookingsBetweenDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Bookings
            .Where(booking =>
                booking.CheckInDate < endDate &&
                booking.CheckOutDate > startDate)
            .ToListAsync();
    }

    public async Task<BookingRequest> CreateBooking(BookingRequest bookingRequest)
    {
        _context.Bookings.Add(bookingRequest);
        await _context.SaveChangesAsync();

        return bookingRequest;
    }
}