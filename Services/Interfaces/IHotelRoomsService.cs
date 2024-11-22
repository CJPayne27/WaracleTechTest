using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Services.Interfaces
{
    public interface IHotelRoomsService
    {
        Task<IEnumerable<HotelRoom>> GetAllHotelRooms();
        Task<IEnumerable<HotelRoom>> GetHotelRoomsByHotelId(string hotelId);
    }
}
