using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Models;
using System.Diagnostics;

namespace OAuth_Project.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly MyAppContext _Context;
        public BookingRepository(MyAppContext context)
        {
            _Context = context;
        }
        public async Task<Booking> AddReservation(Booking reservation)
        {
            if (reservation == null) return null;
            try
            {

            _Context.Add(reservation);
            _Context.SaveChanges();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("BookingRepo",ex);
            }
            return reservation;
        }
       


        public Task<Booking> DeleteReservation(Booking reservation)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetReservation(Booking reservation)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateReservation(Booking reservation)
        {
            throw new NotImplementedException();
        }
    }
}
