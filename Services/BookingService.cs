using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;

namespace HotelWaracleBookingApi.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
    }

    public async Task<Booking?> GetBookingById(Guid bookingId)
    {
        return await _bookingRepository.GetBookingById(bookingId);
    }

    public async Task<Booking> CreateBooking()
    {
        return await _bookingRepository.CreateBooking();
    }
}