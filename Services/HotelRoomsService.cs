using HotelWaracleBookingApi.Data;
using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;

namespace HotelWaracleBookingApi.Services
{
    public class HotelRoomsService : IHotelRoomsService
    {
        private readonly IGenericRepository<HotelRoom> _genericRepository;
        private readonly IHotelRoomsRepository _hotelRoomsRepository;

        public HotelRoomsService(IGenericRepository<HotelRoom> genericRepository, IHotelRoomsRepository hotelRoomsRepository)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _hotelRoomsRepository = hotelRoomsRepository ?? throw new ArgumentNullException(nameof(hotelRoomsRepository));
        }

        public async Task<IEnumerable<HotelRoom>> GetAllHotelRooms()
        {
            return await _genericRepository.GetAllAsync();
        }

        public async Task<IEnumerable<HotelRoom>> GetHotelRoomsByHotelId(string hotelId)
        {
            return await _hotelRoomsRepository.GetByIdAsync(hotelId);
        }
    }
}
