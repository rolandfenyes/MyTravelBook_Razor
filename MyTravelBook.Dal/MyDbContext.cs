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

            // new Trips
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

            // new Accommodations
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
                });

            // Trip - Accommodation
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
                });

            // Accommodation - Participants
            builder.Entity<AccommodationParticipant>().HasData(
                new AccommodationParticipant
                {
                    Id = 1,
                    AccommodationId = 1,
                    UserId = 1
                },
                new AccommodationParticipant
                {
                    Id = 2,
                    AccommodationId = 2,
                    UserId = 1
                });

            // new Travels
            builder.Entity<Travel>().HasData(
                new Travel
                {
                    Id = 1,
                    Departure = "Érd",
                    Destination = "Split",
                    TransportType = TravellingType.Car,
                    TicketPrice = 0F,
                    SeatPrice = 0F,
                    LuggagePrice = 0F,
                    Distance = 734F,
                    Consumption = 6.7F,
                    FuelPrice = 1.33F
                },
                new Travel
                {
                    Id = 2,
                    Departure = "Split",
                    Destination = "Érd",
                    TransportType = TravellingType.Car,
                    TicketPrice = 0F,
                    SeatPrice = 0F,
                    LuggagePrice = 0F,
                    Distance = 734F,
                    Consumption = 6.7F,
                    FuelPrice = 1.33F
                },
                new Travel
                {
                    Id = 3,
                    Departure = "Érd",
                    Destination = "Balatonvilágos",
                    TransportType = TravellingType.Car,
                    TicketPrice = 0F,
                    SeatPrice = 0F,
                    LuggagePrice = 0F,
                    Distance = 76.3F,
                    Consumption = 6.7F,
                    FuelPrice = 1.33F
                },
                new Travel
                {
                    Id = 4,
                    Departure = "Balatonvilágos",
                    Destination = "Érd",
                    TransportType = TravellingType.Car,
                    TicketPrice = 0F,
                    SeatPrice = 0F,
                    LuggagePrice = 0F,
                    Distance = 76.3F,
                    Consumption = 6.7F,
                    FuelPrice = 1.33F
                });

            // Trip - Travel
            builder.Entity<TripTravel>().HasData(
                new TripTravel
                {
                    Id = 1,
                    TripId = 1,
                    TravelId = 1
                },
                new TripTravel
                {
                    Id = 2,
                    TripId = 1,
                    TravelId = 2
                },
                new TripTravel
                {
                    Id = 3,
                    TripId = 2,
                    TravelId = 3
                },
                new TripTravel
                {
                    Id = 4,
                    TripId = 2,
                    TravelId = 4
                });

            // Travel - Participants
            builder.Entity<TravelParticipant>().HasData(
                new TravelParticipant
                {
                    Id = 1,
                    TravelId = 1,
                    UserId = 1
                },
                new TravelParticipant
                {
                    Id = 2,
                    TravelId = 2,
                    UserId = 1
                },
                new TravelParticipant
                {
                    Id = 3,
                    TravelId = 3,
                    UserId = 1
                },
                new TravelParticipant
                {
                    Id = 4,
                    TravelId = 4,
                    UserId = 1
                });

            // new Expenses
            builder.Entity<Expense>().HasData(
                new Expense
                {
                    Id = 1,
                    Location = "Érd",
                    ExpenseName = "Shell",
                    Description = "Hot dog + 2db Hell",
                    Price = 5F
                },
                new Expense
                {
                    Id = 2,
                    Location = "Letenye",
                    ExpenseName = "Benzinkút",
                    Description = "Szendvics",
                    Price = 2F
                }, 
                new Expense
                {
                    Id = 3,
                    Location = "Split",
                    ExpenseName = "McDonalds",
                    Description = "BigTasty menü",
                    Price = 5F
                },
                new Expense
                {
                    Id = 4,
                    Location = "Split",
                    ExpenseName = "Shisha bár",
                    Description = "1db mangó ízű shisha",
                    Price = 10F
                },
                new Expense
                {
                    Id = 5,
                    Location = "Split",
                    ExpenseName = "Hajókirándulás",
                    Description = "Hajókirándulás Hvar szigetére.",
                    Price = 30F
                });

            // Trip - Expense
            builder.Entity<TripExpense>().HasData(
                new TripExpense
                {
                    Id = 1,
                    ExpenseId = 1,
                    TripId = 1
                },
                new TripExpense
                {
                    Id = 2,
                    ExpenseId = 2,
                    TripId = 1
                },
                new TripExpense
                {
                    Id = 3,
                    ExpenseId = 3,
                    TripId = 1
                },
                new TripExpense
                {
                    Id = 4,
                    ExpenseId = 4,
                    TripId = 1
                },
                new TripExpense
                {
                    Id = 5,
                    ExpenseId = 5,
                    TripId = 1
                });

            // Expense - Participant
            builder.Entity<ExpenseParticipants>().HasData(
                new ExpenseParticipants
                {
                    Id = 1,
                    ExpenseId = 1,
                    UserId = 1
                },
                new ExpenseParticipants
                {
                    Id = 2,
                    ExpenseId = 2,
                    UserId = 1
                },
                new ExpenseParticipants
                {
                    Id = 3,
                    ExpenseId = 3,
                    UserId = 1
                },
                new ExpenseParticipants
                {
                    Id = 4,
                    ExpenseId = 4,
                    UserId = 1
                },
                new ExpenseParticipants
                {
                    Id = 5,
                    ExpenseId = 5,
                    UserId = 1
                });

        }
    }
}
