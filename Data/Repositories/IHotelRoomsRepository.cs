using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories
{
    public interface IHotelRoomsRepository
    {
        Task<IEnumerable<HotelRoom>> GetByIdAsync(string id);
    }
}
