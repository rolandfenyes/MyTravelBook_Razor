using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTravelBook.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal
{
    public class MyDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        protected MyDbContext() { }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<AccommodationParticipant> AccommodationParticipants { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseParticipants> ExpenseParticipants { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<TravelParticipant> TravelParticipants { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripAccommodation> TripAccommodations { get; set; }
        public DbSet<TripExpense> TripExpenses { get; set; }
        public DbSet<TripParticipants> TripParticipants { get; set; }
        public DbSet<TripTravel> TripTravels { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Trip>().HasData(
                new Trip
                {
                    Id = 1,
                    TripName = "Split",
                    Starts = new DateTime(2021, 06, 04),
                    Ends = new DateTime(2021, 06, 11),
                    DocumentId = 1,
                    TripOwnerId = 1
                },
                new Trip
                {
                    Id = 2,
                    TripName = "Balaton",
                    Starts = new DateTime(2021, 06, 11),
                    Ends = new DateTime(2021, 06, 13),
                    DocumentId = 2,
                    TripOwnerId = 1
                });

            builder.Entity<Accommodation>().HasData(
                new Accommodation
                {
                    Id = 1,
                    Location = "Split",
                    Starts = new DateTime(2021, 06, 04),
                    Ends = new DateTime(2021, 06, 11),
                    AccommodationType = AccommodationType.RentedAccommodation,
                    PricePerNight = 13.8F
                },
                new Accommodation
                {
                    Id = 2,
                    Location = "Balaton",
                    Starts = new DateTime(2021, 06, 11),
                    Ends = new DateTime(2021, 06, 13),
                    AccommodationType = AccommodationType.OwnApartment,
                    PricePerNight = 0F
                }
                );

            builder.Entity<TripAccommodation>().HasData(
                new TripAccommodation
                {
                    Id = 1,
                    TripId = 1,
                    AccommodationId = 1
                },
                new TripAccommodation
                {
                    Id = 2,
                    TripId = 2,
                    AccommodationId = 2
                }
                );


        }
    }
}
