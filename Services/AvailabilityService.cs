using HotelWaracleBookingApi.Data.Repositories;
using HotelWaracleBookingApi.Models;
using HotelWaracleBookingApi.Services.Interfaces;

namespace HotelWaracleBookingApi.Services;

public class AvailabilityService : IAvailabilityService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHotelRoomRepository _hotelRoomRepository;

    public AvailabilityService(IBookingRepository bookingRepository, IHotelRoomRepository hotelRoomRepository)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _hotelRoomRepository = hotelRoomRepository ?? throw new ArgumentNullException(nameof(hotelRoomRepository));
    }

    /// <summary>
    /// Gets the available hotel rooms for the specified date range and number of guests.
    /// </summary>
    /// <param name="checkInDate">The check-in date.</param>
    /// <param name="checkOutDate">The check-out date.</param>
    /// <param name="numberOfGuests">The number of guests.</param>
    /// <returns>A list of available hotel rooms.</returns>
    public async Task<IEnumerable<HotelRoom>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, int numberOfGuests)
    {
        if (checkInDate >= checkOutDate)
        {
            throw new ArgumentException("Check-out date must be later than check-in date.");
        }

        // Retrieve bookings for the specified date range
        var overlappingBookings = await _bookingRepository.GetAllBookingsBetweenDateRange(checkInDate, checkOutDate);

        // Retrieve available rooms
        var availableRooms = await _hotelRoomRepository.GetAllAsync();

        // Filter rooms
        var filteredRooms = availableRooms
            .Where(room => !room.IsOccupied &&
                           room.MinCapacity <= numberOfGuests &&
                           room.MaxCapacity >= numberOfGuests &&
                           !overlappingBookings.Any(booking =>
                               booking.HotelRoomId == room.Id))
            .ToList();

        return filteredRooms;
    }
}