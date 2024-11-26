using HotelWaracleBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Data.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly HotelWaracleDbContext _context;

    public HotelRepository(HotelWaracleDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Hotel>> GetAllHotels()
    {
        return await _context.Hotels.ToListAsync();
    }

    public async Task<Hotel?> GetHotelByHotelId(string id)
    {
        return await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<Hotel?> GetHotelByName(string name)
    {
        return await _context.Hotels.FirstOrDefaultAsync(h => h.Name == name);
    }
}