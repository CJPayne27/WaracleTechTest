using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data
{
    public partial class DatabaseSeeder
    {
        private readonly HotelWaracleDbContext _context;

        public DatabaseSeeder(HotelWaracleDbContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public async Task SeedAsync()
        {
            try
            {
                await ResetMultipleAsync(typeof(Hotel), typeof(HotelRoom), typeof(Booking));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }

            await _context.Database.EnsureCreatedAsync();

            if (!_context.Hotels.Any())
            {
                var hotels = CreateHotelData();
                await _context.Hotels.AddRangeAsync(hotels);
            }

            if (!_context.HotelRooms.Any())
            {
                var rooms = CreateRoomData();
                await _context.HotelRooms.AddRangeAsync(rooms);
            }

            if (!_context.Bookings.Any())
            {
                var bookings = CreateBookingData();
                await _context.Bookings.AddRangeAsync(bookings);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ResetMultipleAsync(params Type[] entityTypes)
        {
            foreach (var type in entityTypes)
            {
                // Explicitly get the generic Set<TEntity>() method
                var setMethod = _context.GetType()
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == "Set" && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 1);

                if (setMethod == null)
                    throw new InvalidOperationException($"Unable to find a generic Set method for type '{type.Name}'.");

                // Make the method generic for the given type
                var genericSetMethod = setMethod.MakeGenericMethod(type);

                // Invoke the generic Set<TEntity>() method
                var dbSet = genericSetMethod.Invoke(_context, null);

                if (dbSet is IQueryable queryable)
                {
                    var entities = queryable.Cast<object>().ToList();

                    // For Hotels, clear the RoomIds to avoid the issue with required properties
                    foreach (var entity in entities)
                    {
                        if (entity is Hotel hotel)
                        {
                            hotel.RoomIds = [];  
                        }
                    }

                    if (entities.Any())
                    {
                        _context.RemoveRange(entities); // Remove entities
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Failed to retrieve DbSet for type '{type.Name}'.");
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
