using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories;

public interface IHotelRoomRepository
{
    Task<IEnumerable<HotelRoom>> GetAllAsync();
    Task<IEnumerable<HotelRoom>> GetHotelRoomsByHotelIdAsync(string hotelId);
    Task<HotelRoom?> GetHotelRoomByRoomId(string roomId, string hotelId);
}