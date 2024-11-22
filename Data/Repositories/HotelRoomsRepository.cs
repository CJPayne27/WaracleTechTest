using HotelWaracleBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Data.Repositories
{
    public class HotelRoomsRepository : GenericRepository<HotelRoom>, IHotelRoomsRepository
    {
        private readonly HotelWaracleDbContext _context;

        public HotelRoomsRepository(HotelWaracleDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<HotelRoom>> GetAllAsync()
        {
            return await _context.HotelRooms.ToListAsync();
        }
        
        public async Task<IEnumerable<HotelRoom>> GetByIdAsync(string id)
        {
            return await _context.HotelRooms.Where(r => r.HotelId == id).ToListAsync();
        }
    }
}
