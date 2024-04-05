using System.ComponentModel.DataAnnotations.Schema;

namespace OAuth_Project.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string? Destination { get; set; }
        public string? Departure { get; set; }
       
        public Plane? Plane { get; set; }
        [ForeignKey("Plane")]
        public int PlaneId { get; set; }
        public int AvailableSeats { get; set; } = 198;
        public List<FlightSeat>? FlightSeats { get; set; }
        

        //public Flight()
        //{
        //    for (int row = 0; row <= 32; row++)
        //    {
        //        for (int col = 0; col <= 5; col++)
        //        {
        //            FlightSeat seat = new FlightSeat(row, col, this.Id);
        //            this.FlightSeats.Add(seat);
        //        }
        //    }
        //}
    }
}