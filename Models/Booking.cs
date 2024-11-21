namespace HotelWaracleBookingApi.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string HotelRoomId { get; set; } = "";
    }
}
