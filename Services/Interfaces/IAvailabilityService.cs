using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces
{
    public interface IAvailabilityService
    {
        Task<IEnumerable<HotelRoom>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests);
    }
}
