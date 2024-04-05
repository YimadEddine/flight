using Microsoft.AspNetCore.Mvc;
using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IPassengerRepository
    {
        Passenger InsertPassenger(Passenger passenger);
        Passenger UpdatePassenger(Passenger passenger);
        void DeletePassenger(Passenger passenger);
        bool HasFamily(Passenger passenger);
        Passenger GetPassengerById(int id);
    }
}
