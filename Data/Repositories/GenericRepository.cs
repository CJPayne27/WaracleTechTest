using Microsoft.EntityFrameworkCore;

namespace HotelWaracleBookingApi.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class 
{
    protected readonly HotelWaracleDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(HotelWaracleDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}