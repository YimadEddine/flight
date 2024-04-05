using System.ComponentModel.DataAnnotations.Schema;

namespace OAuth_Project.Models
{
    [NotMapped]
    public class BookingDTO
    {
        public Family Family { get; set; }
        public Flight Flight { get; set; }
        public Passenger Reserver { get; set; }
        public List<FlightSeat> Seats { get; set; }
        public int flightId { get; set; }


    }
}
