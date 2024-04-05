using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> AddReservation(Booking reservation);
        Task<Booking> DeleteReservation(Booking reservation);
        Task<Booking> UpdateReservation(Booking reservation);
        Task<Booking> GetReservation(Booking reservation);
    }
}
