using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking?> GetBookingById(Guid bookingId);
        Task<Booking> CreateBooking();
    }
}
