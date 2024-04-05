using OAuth_Project.Models;

namespace OAuth_Project.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> BookFlight(Booking booking);
        void CancelBooking(Booking booking);
        void ChangeBooking(Booking booking);
        List<PassengerCSV> getPassengersCSV(IFormFile file);
        Task<List<Booking>> AddReservationBatch(List<Family> families);
    }
}
