using CsvHelper;
using OAuth_Project.Data;
using OAuth_Project.Interfaces;
using OAuth_Project.Helper_Classes;
using OAuth_Project.Models;
using OAuth_Project.Repositories;
using System.Diagnostics;
using System.Globalization;

namespace OAuth_Project.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly MyAppContext _myAppContext;
        public BookingService(IBookingRepository bookingRepository, IFlightRepository flightRepository,
            IPassengerRepository passengerRepository, IFamilyRepository familyRepository, MyAppContext myAppContext)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
            _familyRepository = familyRepository;
            _myAppContext = myAppContext;


        }
        public async Task<Booking> BookFlight(Booking booking)
        {
            try
            {
                int seatsToDeduct = 0;
                foreach(Passenger p in booking.Family.Members)
                {
                    if (p.NeedsDoubleSpace) seatsToDeduct += 2;
                    else seatsToDeduct += 1;
                }
                Flight bookingFlight = await _flightRepository.GetFlightById(booking.FlightId);
                bookingFlight.AvailableSeats -= seatsToDeduct;

                Family bookingFamily = await _familyRepository.InsertFamily(booking.Family);
                Booking bookingToAdd = new Booking();

                bookingToAdd.Flight = bookingFlight;
                bookingToAdd.FlightId = bookingFlight.Id;
                bookingToAdd.Reserver = booking.Reserver;
                bookingToAdd.Family = bookingFamily;
                bookingToAdd.ReservedSeats = new List<FlightSeat>();

                foreach (FlightSeat seat in booking.ReservedSeats)
                {
                    FlightSeat s;
                    if (seat.SeatName != null)
                    {
                         s = _myAppContext.FlightSeats.Where(s => s.SeatName == seat.SeatName && s.FlightId == bookingFlight.Id).FirstOrDefault();
                        s.Reserved = true;
                    }
                    else
                    {
                         s = _myAppContext.FlightSeats.Where(s => s.Row == seat.Row && s.Column== seat.Column && s.FlightId == bookingFlight.Id).FirstOrDefault();
                        s.Reserved = true;
                    }
                    

                    bookingToAdd.ReservedSeats.Add(s);

                }
                Booking result = await _bookingRepository.AddReservation(bookingToAdd);
                _myAppContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("book flight error:", ex.Message);
                return null;
            }
        }
        public List<PassengerCSV> getPassengersCSV(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    List<PassengerCSV> passengers = csv.GetRecords<PassengerCSV>().ToList();
                    return passengers;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                return null;

            }
        }
        //algo------------------------------------------------------------------------
        public async Task<List<Booking>> AddReservationBatch(List<Family> families)
        {
            int seatsneeded = 0;
            int remainingSeats = 198;
            //array to mimic the plane, indexes 0 not used
            bool[,] seatingArrangement = new bool[7, 34];
            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 33; j++)
                {
                    seatingArrangement[i, j] = false;
                }
            }

            List<Booking> result = new List<Booking>();
            Booking booking;
            FlightSeat seat;
            List<FlightSeat> reservedSeats = new List<FlightSeat>();
            List<Family> normalFamilies = families.Where(f => f.FamilyCode != "-").ToList();
            Family soloPassengersFamily = families.Where(f => f.FamilyCode == "-").FirstOrDefault();
            //this loop wil seat fat people
            int reachedRow = 1;
            int reachedColumn = 1;


            //this loop seats families where there might be children in it
            bool leftToRight = true;
            
            foreach (Family family in normalFamilies)
            {
                
                booking = new Booking();
                booking.Family = family;
                booking.ReservedSeats = new List<FlightSeat>();
                int membersCount = family.Members.Count();
                int childrenCount = BookingHelper.hasChildren(family);


                foreach (Passenger p in family.Members)
                {
                    if (p.NeedsDoubleSpace) seatsneeded += 2;
                    else seatsneeded +=1;
                    bool nextMember = false;
                    for (int column = reachedColumn; column <= 33; column++)
                    {
                        
                        if (leftToRight)
                        {
                            for (int row = reachedRow; row <= 6; row++)
                            {
                                Debug.WriteLine("column : " + column + " row : " + row + " reached column : " + reachedColumn + " reacher row : " + reachedRow);


                                    if (p.NeedsDoubleSpace == true)
                                    {
                                        if (row != 3 && row != 6)
                                        {
                                            seat = new FlightSeat(row, column, p);
                                            FlightSeat secondSeat = new FlightSeat(row + 1, column, p);
                                            booking.ReservedSeats.Add(secondSeat);
                                            booking.ReservedSeats.Add(seat);
                                            if(row == 5)
                                            {
                                                reachedColumn = column+1;
                                                reachedRow = row + 1;
                                            }
                                            else
                                            {
                                                reachedColumn = column ;
                                                reachedRow = row + 2;
                                            }
                                            
                                            seatingArrangement[row, column ] = true;
                                            seatingArrangement[row + 1, column ] = true;

                                            nextMember = true;
                                        }
                                        else if (row == 3)
                                        {
                                            seat = new FlightSeat(row + 1, column, p);
                                            FlightSeat secondSeat = new FlightSeat(row + 2, column, p);
                                            seatingArrangement[row+2, column ] = true;
                                            seatingArrangement[row + 1, column ] = true;
                                            booking.ReservedSeats.Add(secondSeat);
                                            booking.ReservedSeats.Add(seat);
                                            reachedColumn = column;
                                            reachedRow = row + 3;
                                            nextMember = true;
                                        }
                                        else if (row == 6)
                                        {
                                            if (column < 33)
                                            {
                                                seat = new FlightSeat(row, column + 1, p);
                                                FlightSeat secondSeat = new FlightSeat(row - 1, column + 1, p);
                                                seatingArrangement[row - 1, column+1] = true;
                                                seatingArrangement[row , column+1] = true;
                                                booking.ReservedSeats.Add(secondSeat);
                                                booking.ReservedSeats.Add(seat);
                                                reachedColumn = column;
                                                reachedRow = row + 2;
                                                nextMember = true;
                                            }

                                        }
                                    }
                                    else if(!p.NeedsDoubleSpace)
                                    {
                                        seat = new FlightSeat(row, column , p);
                                        seatingArrangement[row , column] = true;
                                        booking.ReservedSeats.Add(seat);
                                        if(row== 6)
                                        {
                                            reachedColumn = column+1;
                                            reachedRow = row;
                                        leftToRight = false;
                                        }
                                        else
                                        {
                                            reachedColumn = column ;
                                            reachedRow = row+1;
                                        }
                                        
                                        nextMember = true;
                                    }

                                if (nextMember) { break; }
                            }
                        }
                        else if (!leftToRight)
                        {
                            for (int row = reachedRow; row >= 1; row--)
                            {
                                Debug.WriteLine("column : " + column + " row : " + row + " reached column : " + reachedColumn + " reacher row : " + reachedRow);

                                if (p.NeedsDoubleSpace)
                                {
                                    if (row != 4 && row != 1)
                                    {
                                        seat = new FlightSeat(row-1, column, p);
                                        FlightSeat secondSeat = new FlightSeat(row , column, p);
                                        booking.ReservedSeats.Add(secondSeat);
                                        booking.ReservedSeats.Add(seat);
                                        if(row == 2)
                                        {
                                            reachedColumn = column+1;
                                            reachedRow = row -1;
                                        }
                                        else
                                        {
                                            reachedColumn = column;
                                            reachedRow = row -2;
                                        }
                                        seatingArrangement[row, column ] = true;
                                        seatingArrangement[row - 1, column ] = true;

                                        nextMember = true;
                                    }
                                    else if (row == 4)
                                    {
                                        seat = new FlightSeat(row - 1, column, p);
                                        FlightSeat secondSeat = new FlightSeat(row - 2, column, p);
                                        seatingArrangement[row -1, column ] = true;
                                        seatingArrangement[row -2, column ] = true;
                                        booking.ReservedSeats.Add(secondSeat);
                                        booking.ReservedSeats.Add(seat);
                                        reachedColumn = column;
                                        reachedRow = row -3;
                                        nextMember = true;
                                    }
                                    else if (row == 1)
                                    {
                                        if (column < 33 )
                                        {
                                            seat = new FlightSeat(row, column + 1, p);
                                            FlightSeat secondSeat = new FlightSeat(row + 1, column + 1, p);
                                            seatingArrangement[row , column] = true;
                                            seatingArrangement[row+1, column] = true;
                                            booking.ReservedSeats.Add(secondSeat);
                                            booking.ReservedSeats.Add(seat);
                                            reachedColumn = column;
                                            reachedRow = row + 2;
                                            nextMember = true;
                                        }

                                    }
                                }
                                else if(!p.NeedsDoubleSpace)
                                {
                                    seat = new FlightSeat(row, column, p);
                                    seatingArrangement[row, column] = true;
                                    booking.ReservedSeats.Add(seat);
                                    if (row == 1)
                                    {
                                        reachedColumn = column + 1;
                                        reachedRow = row;
                                        leftToRight = true;
                                    }
                                    else
                                    {
                                        reachedColumn = column;
                                        reachedRow = row - 1;
                                    }

                                    nextMember = true;
                                }



                                if (nextMember) { break; }
                            }
                        }
                        
                        
                        if (nextMember) { break; }
                    }
                    
                }
                result.Add(booking);

            }
            //loop for fatties and non fatties solos
             reachedRow = 1;
             reachedColumn = 1;
            booking = new Booking();
            booking.ReservedSeats = new List<FlightSeat>();
            booking.Family = soloPassengersFamily;
            foreach(Passenger p in soloPassengersFamily.Members.Where(p=>p.NeedsDoubleSpace==true).ToList())
            {
                seatsneeded += 2;
                bool nextMember = false;
                for(int column = reachedColumn; column <= 33; column++)
                {
                    for(int row=reachedRow;row<=6;row++)
                    {
                        if (row != 6 && row != 3)
                        {
                            if (seatingArrangement[row, column] == false && seatingArrangement[row + 1, column] == false)
                            {
                                FlightSeat seat1 = new FlightSeat(row, column, p);
                                FlightSeat seat2 = new FlightSeat(row + 1, column, p);
                                seatingArrangement[row, column] = true;
                                seatingArrangement[row+1, column] = true;
                                booking.ReservedSeats.Add(seat1);
                                booking.ReservedSeats.Add(seat2);
                                nextMember = true;
                                if(row == 5)
                                {

                                    reachedColumn=column+1;
                                    reachedRow = 1;
                                }
                                else
                                {
                                    reachedColumn = column;
                                    reachedRow=row+2;
                                }
                            }
                        }
                        if(nextMember) { break; }
                    }
                    if (nextMember) { break; }
                }
            }
            reachedRow = 1;
            reachedColumn = 1;
            foreach (Passenger p in soloPassengersFamily.Members.Where(p => p.NeedsDoubleSpace == false).ToList())
            {
                seatsneeded += 1;
                bool nextMember = false;
                for (int column = reachedColumn; column <= 33; column++)
                {
                    for (int row = reachedRow; row <= 6; row++)
                    {
                        
                            if (seatingArrangement[row, column] == false)
                            {
                                FlightSeat seat1 = new FlightSeat(row, column, p);
                               
                                booking.ReservedSeats.Add(seat1);
                                
                                nextMember = true;
                                if (row == 6)
                                {

                                    reachedColumn = column + 1;
                                    reachedRow = 1;
                                }
                                else
                                {
                                    reachedColumn = column;
                                    reachedRow = row + 1;
                                }
                            }
                        
                        if (nextMember) { break; }
                    }
                    if (nextMember) { break; }
                }
            }
            int seatsFilled = 0;
            
            result.Add(booking);
            foreach(Booking b in result)
            {
                seatsFilled += b.ReservedSeats.Count;
            }
            return result;
        }






        public void CancelBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void ChangeBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
