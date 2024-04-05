using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OAuth_Project.Models;
using System;

namespace OAuth_Project.Data
{
    public class MyAppContext:IdentityDbContext<AppUser>
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightSeat> FlightSeats { get; set; }


        public MyAppContext(DbContextOptions<MyAppContext> options):base (options)
        {
            
        }
    }
}
