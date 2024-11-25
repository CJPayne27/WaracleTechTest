using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Repositories;

public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<Hotel?> GetHotelByHotelId(int id);
    Task<Hotel?> GetHotelByName(string name);
}