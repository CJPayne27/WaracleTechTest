using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;

namespace HotelWaracleBookingApi.Services;

public class HotelRoomService : IHotelRoomService
{
    private readonly IGenericRepository<HotelRoom> _genericRepository;
    private readonly IHotelRoomRepository _hotelRoomRepository;

    public HotelRoomService(IGenericRepository<HotelRoom> genericRepository, IHotelRoomRepository hotelRoomRepository)
    {
        _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        _hotelRoomRepository = hotelRoomRepository ?? throw new ArgumentNullException(nameof(hotelRoomRepository));
    }

    public async Task<IEnumerable<HotelRoom>> GetAllHotelRooms()
    {
        return await _genericRepository.GetAllAsync();
    }

    public async Task<IEnumerable<HotelRoom>> GetHotelRoomsByHotelId(string hotelId)
    {
        return await _hotelRoomRepository.GetHotelRoomsByHotelIdAsync(hotelId);
    }

    public async Task<HotelRoom?> GetHotelRoomByRoomId(string roomId, string hotelId)
    {
        return await _hotelRoomRepository.GetHotelRoomByRoomId(roomId, hotelId);
    }
}