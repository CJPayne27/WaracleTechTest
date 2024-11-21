using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data;

public partial class DatabaseSeeder
{
    private List<Booking> CreateBookingData()
    {
        var bookings = new List<Booking>
        {
            new()
            {
                Id = Guid.Parse("ab6a49ab-c2dd-4eac-ad27-0c69e296a714"),
                BookingDate = new DateTime(2024, 11, 15),
                CheckInDate = new DateTime(2024, 12, 15),
                CheckOutDate = new DateTime(2024, 12, 17),
                NumberOfGuests = 3,
                HotelRoomId = "DL1103"
            },
            new()
            {
                Id = Guid.Parse("3aa61f21-489e-4b0e-8863-0257041d5ad5"),
                BookingDate = new DateTime(2024, 11, 17),
                CheckInDate = new DateTime(2025, 01, 17),
                CheckOutDate = new DateTime(2025, 01, 20),
                NumberOfGuests = 2,
                HotelRoomId = "AG102"
            },
            new()
            {
                Id = Guid.Parse("50f18776-20be-4e88-9c19-86d17854a6e7"),
                BookingDate = new DateTime(2024, 11, 19),
                CheckInDate = new DateTime(2025, 02, 21),
                CheckOutDate = new DateTime(2025, 02, 28),
                NumberOfGuests = 6,
                HotelRoomId = "AG106"
            },
            new()
            {
                Id = Guid.Parse("53a77db7-81e7-4cf9-8233-875c63812ac9"),
                BookingDate = new DateTime(2024, 11, 20),
                CheckInDate = new DateTime(2025, 02, 01),
                CheckOutDate = new DateTime(2025, 02, 15),
                NumberOfGuests = 8,
                HotelRoomId = "AL206"
            },
            new()
            {
                Id = Guid.Parse("973e9d13-daf0-4572-a2d9-4a283913ed78"),
                BookingDate = new DateTime(2024, 11, 21),
                CheckInDate = new DateTime(2025, 01, 01),
                CheckOutDate = new DateTime(2025, 01, 02),
                NumberOfGuests = 1,
                HotelRoomId = "AB332"
            }
        };

        return bookings;
    }
}