using OAuth_Project.Models;
using OAuth_Project.Repositories;

namespace OAuth_Project.Interfaces
{
    public interface IFlightRepository
    {
        
        Task<List<Flight>> GetAllFlightsAvailable();
        Task<Flight> GetFlightById(int id);
        Flight CreateRandomFlight();
    }
}
