using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingRequest?> GetBookingById(Guid bookingId);
        Task<BookingRequest> CreateBooking(BookingRequest bookingRequest);
        Task<IEnumerable<BookingRequest>> GetAllBookingsBetweenDateRange(DateTime bookingRequestCheckInDate,
            DateTime bookingRequestCheckOutDate);
    }
}
