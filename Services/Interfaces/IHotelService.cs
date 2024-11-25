using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces;

public interface IHotelService
{
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelByHotelId(int id);
    Task<Hotel> GetHotelByName(string name);
}