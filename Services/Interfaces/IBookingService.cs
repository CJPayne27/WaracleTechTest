using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Booking?> GetBookingById(Guid bookingId);
        Task<Booking> CreateBooking();
    }
}
