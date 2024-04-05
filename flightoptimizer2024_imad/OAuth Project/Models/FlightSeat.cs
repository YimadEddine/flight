namespace OAuth_Project.Models
{
    public class FlightSeat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        public bool Reserved { get; set; } = false;
        
        public Passenger? SeatPassenger { get; set; }
        public int? SeatPassengerId { get; set; }
        public string? SeatName {  get; set; }
        public FlightSeat(int row, int column, int flightId)
        {
            this.Row = row;   
            this.Column = column;   
            this.FlightId = flightId;
        }
        public FlightSeat(int row, int column, Passenger pass = null)
        {
            this.Row = row;
            this.Column = column;
            this.SeatPassenger = pass;
        }

        public FlightSeat()
        {
            
        }
    }
}