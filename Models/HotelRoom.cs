namespace HotelWaracleBookingApi.Models
{
    public class HotelRoom
    {
        public string Id { get; set; } = "";
        public string HotelId { get; set; } = "";
        public int RoomNumber { get; set; }
        public string RoomType { get; set; } = "";
        public int MaxCapacity { get; set; }
        public int MinCapacity { get; set; }
        public bool IsOccupied { get; set; }
    }
}
