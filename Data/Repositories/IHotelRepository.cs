using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories;

public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<Hotel?> GetHotelByHotelId(string id);
    Task<Hotel?> GetHotelByName(string name);
}