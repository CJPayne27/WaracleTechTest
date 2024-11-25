using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;

namespace HotelWaracleBookingApi.Services;

public class HotelService : IHotelService
{
    private readonly IGenericRepository<Hotel> _genericRepository;
    private readonly IHotelRepository _hotelRepository;


    public HotelService(IGenericRepository<Hotel> genericRepository, IHotelRepository hotelRepository)
    {
        _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
    }

    public async Task<IEnumerable<Hotel>> GetAllHotels()
    {
        return await _genericRepository.GetAllAsync();
    }

    public async Task<Hotel> GetHotelByHotelId(int id)
    {
        return await _hotelRepository.GetHotelByHotelId(id);
    }

    public async Task<Hotel> GetHotelByName(string name)
    {
        return await _hotelRepository.GetHotelByName(name);
    }
}