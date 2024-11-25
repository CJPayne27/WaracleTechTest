namespace HotelWaracleBookingApi.Models;

public class Hotel
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string City { get; set; } = "";
    public string Description { get; set; } = "";
    public required List<string> RoomIds { get; set; }
}