using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces;

public interface IHotelRoomService
{
    Task<IEnumerable<HotelRoom>> GetAllHotelRooms();
    Task<IEnumerable<HotelRoom>> GetHotelRoomsByHotelId(string hotelId);
}