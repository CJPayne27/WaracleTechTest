using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces;

public interface IHotelService
{
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<Hotel?> GetHotelByHotelId(string id);
    Task<Hotel?> GetHotelByName(string name);
}