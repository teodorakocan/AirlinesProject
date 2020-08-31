using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Authentication
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Airline_Admin", NormalizedName = "Airline_Admin".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Service_Admin", NormalizedName = "Service_Admin".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "User".ToUpper() });

        }

        public DbSet<MyUser> MyUsers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<AircraftConfiguration> AircraftConfigurations { get; set; }
        public DbSet<AirlineDestinationConnection> AirlineDestinations { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<FastReservation> FastReservations { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListServiceConnection> PriceListServices { get; set; }
        public DbSet<RentACar> RentACars { get; set; }
        public DbSet<RentaPriceListServiceConnection> RentaPriceListServices { get; set; }
        public DbSet<RentPriceList> RentPriceLists { get; set; }
        public DbSet<RentService> RentServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<CarReservation> CarReservations { get; set; }
        public DbSet<Discounts> Discounts { get; set; }
    }
}
