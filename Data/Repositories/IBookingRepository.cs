using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories;

public interface IBookingRepository
{
    Task<BookingRequest?> GetBookingById(Guid bookingId);
    Task<IEnumerable<BookingRequest?>> GetAllBookingsBetweenDateRange(DateTime startDate, DateTime endDate);
    Task<BookingRequest> CreateBooking(BookingRequest bookingRequest);
}