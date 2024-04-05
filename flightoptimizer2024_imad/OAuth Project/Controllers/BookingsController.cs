using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth_Project.Interfaces;
using OAuth_Project.Model_Binders;
using OAuth_Project.Models;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;

namespace OAuth_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightRepository _flightService;
        public BookingsController(IBookingService bookingService, IFlightRepository flight)
        {
            _bookingService = bookingService;
            _flightService = flight;
        }

        [HttpPost]
        public async Task<IActionResult> BookFlightAsync([ModelBinder(BinderType = typeof(BookingModelBinder))] Booking booking)
        {
            try
            {
                if (booking != null)
                {
                    Booking result = await _bookingService.BookFlight(booking);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return BadRequest();


                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return BadRequest();
            }
        }
        [HttpPost("csv")]
        public async Task<IActionResult> BookFlightBatchAsync(IFormFile file)
        {
            Flight createdFlight = _flightService.CreateRandomFlight();
            List<PassengerCSV> CSV = this._bookingService.getPassengersCSV(file);
            List<Passenger> passengers = new List<Passenger>();
            List<Family> families = new List<Family>();
            List<Booking> bookingsResult = new List<Booking>();
            float totalPrice = 0;
            foreach (PassengerCSV p in CSV)
            {
                Family family;
                Passenger passenger = new Passenger(p);
                passengers.Add(passenger);

                Family exists = families.Where(f=>f.FamilyCode == p.FamilyCode).FirstOrDefault();
                    if (exists == null)
                    {
                        if (p.FamilyCode == "-")
                        {
                            family = new Family("-");

                        }
                        else
                        {
                            family = new Family(p.FamilyCode);
                        }
                    }
                    else { continue; }

                
                families.Add(family);
            }
            foreach(Family f in families)
            {
                f.Members = passengers.Where(p=> p.familyCode == f.FamilyCode).ToList();
            }
            try
            {
              
                 bookingsResult = await _bookingService.AddReservationBatch(families);
                 foreach(Booking booking in bookingsResult)
                 {
                    booking.FlightId=createdFlight.Id;
                    foreach(Passenger p in booking.Family.Members)
                    {
                        if(p.NeedsDoubleSpace)
                        {
                            booking.totalPrice += 500;
                        }else if(p.Type == "Child")
                        {
                            booking.totalPrice += 150;
                        }
                        else { booking.totalPrice += 250; }
                    }
                    foreach(FlightSeat seat in booking.ReservedSeats)
                    {
                        seat.FlightId = createdFlight.Id;
                    }
                    totalPrice +=(float) booking.totalPrice;
                    await BookFlightAsync(booking);
                 }
                Flight updatedFlight = await _flightService.GetFlightById(createdFlight.Id);
                if(bookingsResult.Count > 0)
                {
                    return Ok(new {totalprice=totalPrice, flight=updatedFlight});
                }

            }catch(Exception ex)
            {
                Debug.WriteLine("batch error : ",ex.Message);
            }
           


            return Ok();
        }

    }
}
