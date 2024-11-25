using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories;

public interface IHotelRoomRepository
{
    Task<IEnumerable<HotelRoom>> GetByIdAsync(string id);
}