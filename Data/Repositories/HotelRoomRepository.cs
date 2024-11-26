using HotelWaracleBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Data.Repositories;

public class HotelRoomRepository : IHotelRoomRepository
{
    private readonly HotelWaracleDbContext _context;

    public HotelRoomRepository(HotelWaracleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HotelRoom>> GetAllAsync()
    {
        return await _context.HotelRooms.ToListAsync();
    }

    public async Task<IEnumerable<HotelRoom>> GetHotelRoomsByHotelIdAsync(string hotelId)
    {
        return await _context.HotelRooms.Where(r => r.HotelId == hotelId).ToListAsync();
    }

    public async Task<HotelRoom?> GetHotelRoomByRoomId(string roomId, string hotelId)
    {
        return await _context.HotelRooms.Where(r => r.HotelId == hotelId && r.Id == roomId).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<HotelRoom>> GetByIdAsync(string id)
    {
        return await _context.HotelRooms.Where(r => r.HotelId == id).ToListAsync();
    }
}