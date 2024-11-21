using HotelWaracleBookingApi.Models;

namespace HotelWaracleBookingApi.Data.Seeding;

public partial class DatabaseSeeder
{
    private static List<HotelRoom> CreateRoomData()
    {
        var rooms = new List<HotelRoom>
        {
            new()
            {
                HotelId = "ALP001",
                Id = "AG101",
                RoomNumber = 101,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 1,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP001",
                Id = "AG102",
                RoomNumber = 102,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP001",
                Id = "AG103",
                RoomNumber = 103,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 4,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP001",
                Id = "AG104",
                RoomNumber = 104,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 4,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP001",
                Id = "AG105",
                RoomNumber = 105,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 4,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP001",
                Id = "AG106",
                RoomNumber = 106,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 6,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP002",
                Id = "AL201",
                RoomNumber = 201,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 1,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP002",
                Id = "AL202",
                RoomNumber = 202,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP002",
                Id = "AL203",
                RoomNumber = 203,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP002",
                Id = "AL204",
                RoomNumber = 204,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 4,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP002",
                Id = "AL205",
                RoomNumber = 205,
                RoomType = "Deluxe",
                MinCapacity = 1,
                MaxCapacity = 6,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP002",
                Id = "AL206",
                RoomNumber = 206,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 8,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP003",
                Id = "AB331",
                RoomNumber = 331,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 1,
                IsOccupied = false
            },
            new()
            {
                HotelId = "ALP003",
                Id = "AB332",
                RoomNumber = 332,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 1,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP003",
                Id = "AB333",
                RoomNumber = 333,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP003",
                Id = "AB334",
                RoomNumber = 334,
                RoomType = "Deluxe",
                MinCapacity = 1,
                MaxCapacity = 4,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP003",
                Id = "AB335",
                RoomNumber = 335,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 6,
                IsOccupied = true
            },
            new()
            {
                HotelId = "ALP003",
                Id = "AB336",
                RoomNumber = 336,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 8,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET010",
                Id = "BE401",
                RoomNumber = 401,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 1,
                IsOccupied = true
            },
            new()
            {
                HotelId = "BET010",
                Id = "BE402",
                RoomNumber = 402,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = true
            },
            new()
            {
                HotelId = "BET010",
                Id = "BE403",
                RoomNumber = 403,
                RoomType = "Double",
                MinCapacity = 2,
                MaxCapacity = 4,
                IsOccupied = true
            },
            new()
            {
                HotelId = "BET010",
                Id = "BE404",
                RoomNumber = 404,
                RoomType = "Double",
                MinCapacity = 2,
                MaxCapacity = 6,
                IsOccupied = true
            },
            new()
            {
                HotelId = "BET010",
                Id = "BE405",
                RoomNumber = 405,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 6,
                IsOccupied = true
            },
            new()
            {
                HotelId = "BET010",
                Id = "BE406",
                RoomNumber = 406,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 6,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET020",
                Id = "BA511",
                RoomNumber = 511,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 1,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET020",
                Id = "BA512",
                RoomNumber = 512,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET020",
                Id = "BA513",
                RoomNumber = 513,
                RoomType = "Single",
                MinCapacity = 1,
                MaxCapacity = 2,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET020",
                Id = "BA514",
                RoomNumber = 514,
                RoomType = "Double",
                MinCapacity = 1,
                MaxCapacity = 4,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET020",
                Id = "BA515",
                RoomNumber = 515,
                RoomType = "Double",
                MinCapacity = 2,
                MaxCapacity = 4,
                IsOccupied = false
            },
            new()
            {
                HotelId = "BET020",
                Id = "BA516",
                RoomNumber = 516,
                RoomType = "Deluxe",
                MinCapacity = 2,
                MaxCapacity = 5,
                IsOccupied = false
            }
        };

        return rooms;
    }
}