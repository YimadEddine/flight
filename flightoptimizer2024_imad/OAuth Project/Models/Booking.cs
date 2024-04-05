using System.ComponentModel.DataAnnotations.Schema;

namespace OAuth_Project.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int? ReserverId {  get; set; }
        public Passenger? Reserver { get; set; }
        public Flight? Flight { get; set; }
        public Family? Family { get; set; }
        public int? FamilyId { get; set; }
        [ForeignKey("Flight")]
        public int FlightId { get;set; }
        public bool Canceled { get; set; } = false;
        public List<FlightSeat>? ReservedSeats { get; set; }
        public float? totalPrice { get; set; } = 0;
    }
}
