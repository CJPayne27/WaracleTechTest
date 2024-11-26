using System.Text.Json.Serialization;

namespace HotelWaracleBookingApi.Models;

public class BookingRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
    public string HotelRoomId { get; set; } = "";
    public string HotelId { get; set; } = "";
    [JsonIgnore]
    public string Status { get; set; } = "";
}