using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;

namespace OAuth_Project.Repositories
{
    public class PassengerRepository: IPassengerRepository
    {
        private readonly MyAppContext _context;
        public PassengerRepository(MyAppContext context)
        {
            this._context = context;
        }

        public Passenger InsertPassenger(Passenger passenger)
        {
            if (passenger == null) return null;
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
            return passenger;
        }

        public void DeletePassenger(Passenger passenger)
        {
            throw new NotImplementedException();
        }

        public Passenger GetPassengerById(int id)
        {
            return _context.Passengers.Find(id);
        }

        public bool HasFamily(Passenger passenger)
        {
            throw new NotImplementedException();
        }

      

        public Passenger UpdatePassenger(Passenger passenger)
        {
            throw new NotImplementedException();
        }
    }
}
