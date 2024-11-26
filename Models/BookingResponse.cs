namespace HotelWaracleBookingApi.Models
{
    public class BookingResponse
    { 
        public Guid Id { get; set; }
        public string Message { get; set; } = "";
        public string ErrorDetails { get; set; } = "";
    }
}
