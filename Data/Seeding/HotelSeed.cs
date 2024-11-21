using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data;

public partial class DatabaseSeeder
{
    private static List<Hotel> CreateHotelData()
    {
        var hotels = new List<Hotel>
        {
            new()
            {
                Id = "ALP001",
                Name = "Hotel Alpha-Glasgow",
                City = "Glasgow",
                Description = "A luxurious hotel in the heart of Glasgow.",
                RoomIds = ["AG101", "AG102", "AG103", "AG104", "AG105", "AG106"]
            },
            new()
            {
                Id = "ALP002",
                Name = "Hotel Alpha-Liverpool",
                City = "Liverpool",
                Description = "Another luxurious hotel in the heart of Liverpool.",
                RoomIds = ["AL201", "AL202", "AL203", "AL204", "AL205", "AL206"]
            },
            new()
            {
                Id = "ALP003",
                Name = "Hotel Alpha-Brighton",
                City = "Brighton",
                Description = "Yet another luxurious hotel in the heart of Newcastle.",
                RoomIds = ["AB331", "AB332", "AB333", "AB334", "AB335", "AB336"]
            },
            new()
            {
                Id = "BET010",
                Name = "Hotel Beta-Edinburgh",
                City = "Edinburgh",
                Description = "A pretty nice hotel in the heart of Edinburgh.",
                RoomIds = ["BE401", "BE402", "BE403", "BE404", "BE405", "BE406"]
            },
            new()
            {
                Id = "BET020",
                Name = "Hotel Beta-Aberdeen",
                City = "Aberdeen",
                Description = "Another pretty nice hotel in the heart of Aberdeen.",
                RoomIds = ["BA511", "BA512", "BA513", "BA514", "BA515", "BA516"]
            },
            new()
            {
                Id = "BET030",
                Name = "Hotel Beta-Manchester",
                City = "Manchester",
                Description = "Yet another luxurious hotel in the heart of Newcastle.",
                RoomIds = ["BM651", "BM652", "BM653", "BM654", "BM655", "BM656"]
            },
            new()
            {
                Id = "DEL100",
                Name = "Hotel Delta-London",
                City = "London",
                Description = "A reasonably nice hotel in the heart of London.",
                RoomIds = ["DL1101", "DL1102", "DL1103", "DL1104", "DL1105", "DL1106"]
            },
            new()
            {
                Id = "DEL200",
                Name = "Hotel Delta-Inverness",
                City = "Inverness",
                Description = "Another reasonably nice hotel in the heart of Inverness.",
                RoomIds = ["DI2101", "DI2102", "DI2103", "DI2104", "DI2105", "DI2106"]
            },
            new()
            {
                Id = "DEL300",
                Name = "Hotel Delta-Newcastle",
                City = "Newcastle",
                Description = "Yet another reasonably nice hotel in the heart of Newcastle.",
                RoomIds = ["DN4501", "DN4502", "DN4503", "DN4504", "DN4505", "DN4506"]
            }
        };

        return hotels;
    }
}