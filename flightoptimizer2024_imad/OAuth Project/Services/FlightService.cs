using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Migrations;
using OAuth_Project.Models;
using OAuth_Project.Repositories;

namespace OAuth_Project.Services
{
    public class FlightService : IFlightService
    {
    
        private readonly IFlightRepository _flightRepository;
        public FlightService(IFlightRepository flightRepository)
        {
            this._flightRepository=flightRepository;
        }
        public async Task<List<Flight>> GetAllAvailableFlights()
        {
            //List<Flight> resut = await _flightRepository.GetAllFlightsAvailable();
            return await _flightRepository.GetAllFlightsAvailable();
        }
        public async Task<Flight> GetFlightById(int id)
        {
            return await _flightRepository.GetFlightById(id);
        }
    }
}
