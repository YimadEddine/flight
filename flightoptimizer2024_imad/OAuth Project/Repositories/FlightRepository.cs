using Microsoft.EntityFrameworkCore;
using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;

namespace OAuth_Project.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly MyAppContext   _context;
        public FlightRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetAllFlightsAvailable()
        {
            return await _context.Flights.Include(f=>f.Plane).Include(f=>f.FlightSeats).ToListAsync();
        }

        public async Task<Flight> GetFlightById(int id)
        {
            Flight result = await _context.Flights.Include(f=>f.FlightSeats).Where(f => f.Id == id).FirstOrDefaultAsync();
            if (result == null) return null;
            return result;
        }

        public Flight CreateRandomFlight()
        {
            Flight flight = new Flight();
            Plane plane = _context.Planes.First();
            flight.Departure = "departure " + _context.Flights.Count();
            flight.Destination = "destination " + _context.Flights.Count();
            flight.AvailableSeats = 198;
            flight.Plane = plane;
            flight.PlaneId=plane.Id;
            _context.Flights.Add(flight);
            _context.SaveChanges();
            return flight;

        }
    }
}
