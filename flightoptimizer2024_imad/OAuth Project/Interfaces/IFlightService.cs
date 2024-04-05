using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllAvailableFlights();
        Task<Flight> GetFlightById(int id);
    }
}
