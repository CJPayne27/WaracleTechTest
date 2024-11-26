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

    public async Task<BookingRequest?> GetBookingById(Guid bookingId)
    {
        return await _bookingRepository.GetBookingById(bookingId);
    }

    public async Task<BookingRequest> CreateBooking(BookingRequest bookingRequest)
    {
        return await _bookingRepository.CreateBooking(bookingRequest);
    }

    public async Task<IEnumerable<BookingRequest>> GetAllBookingsBetweenDateRange(DateTime bookingRequestCheckInDate,
        DateTime bookingRequestCheckOutDate)
    {
        return await _bookingRepository.GetAllBookingsBetweenDateRange(bookingRequestCheckInDate, bookingRequestCheckOutDate);
    }
}