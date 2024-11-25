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

    public async Task<Booking?> GetBookingById(Guid bookingId)
    {
        return await _context.Bookings.FirstOrDefaultAsync(b => b!.Id == bookingId);
    }

    public Task<Booking> CreateBooking()
    {
        throw new NotImplementedException(); //TODO: Implement
    }
}