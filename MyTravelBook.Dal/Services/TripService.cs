using Microsoft.EntityFrameworkCore;
using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTravelBook.Dal.Services
{
    public class TripService
    {
        public MyDbContext DbContext { get; }
        public TravelService TravelService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public ExpenseService ExpenseService { get; set; }
        public TripService(MyDbContext dbContext, TravelService travelService, AccommodationService accommodationService, ExpenseService expenseService)
        {
            DbContext = dbContext;
            TravelService = travelService;
            AccommodationService = accommodationService;
            ExpenseService = expenseService;
        }

        // Create

        public void CreateNewTrip(CreateTripHeader trip)
        {
            var tripEntity = new Trip
            {
                TripName = trip.TripName,
                Starts = trip.Starts,
                Ends = trip.Ends,
                DocumentId = null,
                TripOwnerId = trip.TripOwnerId
            };
            var documentsEntity = new Document
            {
                IdCard = trip.TripDetails.IDCard,
                InternationalPassport = trip.TripDetails.InternationalPassport,
                DrivingLicense = trip.TripDetails.DrivingLicense,
                HealthCard = trip.TripDetails.HealthCard
            };
            DbContext.Documents.Add(documentsEntity);
            DbContext.SaveChanges();
            tripEntity.DocumentId = documentsEntity.Id;
            DbContext.Trips.Add(tripEntity);
            DbContext.SaveChanges();
        }

        // Read

        public ExpensesHeader GetExpensesOfTrip(int tripId)
        {
            var expenseIds = DbContext.TripExpenses.Where(t => t.TripId == tripId).ToList();
            var expensesOfTrip = new ExpensesHeader();
            foreach (var expenseId in expenseIds)
            {
                expensesOfTrip.Expenses.Add(ExpenseService.GetExpense(expenseId.ExpenseId));
            }
            return expensesOfTrip;
        }

        public AccommodationsHeader GetAccommodationsOfTrip(int tripId)
        {
            var accommodationIds = DbContext.TripAccommodations.Where(t => t.TripId == tripId).ToList();
            var accommodationsOfTrip = new AccommodationsHeader();
            foreach (var accommodationId in accommodationIds)
            {
                accommodationsOfTrip.Accommodations.Add(AccommodationService.GetAccommodation(tripId));
            }
            return accommodationsOfTrip;
        }

        public TravelsHeader GetTravelsOfTrip(int tripId)
        {
            var travelIds= DbContext.TripTravels.Where(t => t.TripId == tripId).ToList();
            var travelsOfTrip = new TravelsHeader();
            foreach (var travelId in travelIds)
            {
                travelsOfTrip.Travels.Add(TravelService.GetTravel(travelId.TravelId));
            }
            return travelsOfTrip;
        }

        public IEnumerable<FriendHeader> GetParticipantsOfTrip(int tripId)
        {
            var participants = DbContext.TripParticipants.Where(t => t.TripId == tripId).ToList();

            var participantsInTrip = new List<FriendHeader>();
            foreach (var user in participants)
            {
                participantsInTrip.Add(
                    new FriendHeader
                    {
                        FriendId = user.UserId,
                        Nickname = DbContext.Users.Where(u => u.Id == user.UserId).FirstOrDefault().Name
                    });
            }
            return participantsInTrip;
        }

        public TripDetailsHeader GetDetailsOfTrip(int tripId)
        {
            var documentId = DbContext.Trips.Where(t => t.Id == tripId).FirstOrDefault().DocumentId;
            var tripDetails = DbContext.Documents.Where(d => d.Id == documentId).FirstOrDefault();
            return new TripDetailsHeader
            {
                TripId = tripId,
                IDCard = tripDetails.IdCard,
                InternationalPassport = tripDetails.InternationalPassport,
                DrivingLicense = tripDetails.DrivingLicense,
                HealthCard = tripDetails.HealthCard
            };

        }

        public TripHeader GetTrip(int id)
        {
            var trip = DbContext.Trips.Where(t => t.Id == id).FirstOrDefault();
            return new TripHeader
            {
                Id = trip.Id,
                Location = null,
                Progress = null,
                TripName = trip.TripName
            };
        }

        public async Task<IEnumerable<TripHeader>> GetTrips(int userId)
        {
            var tripsOfUser = await DbContext.TripParticipants.Where(t => t.UserId == userId).ToListAsync();

            var tripEntities = new List<Trip>();

            foreach (var id in tripsOfUser)
            {
                tripEntities.Add(DbContext.Trips.Where(t => t.Id == id.TripId).FirstOrDefault());
            }

            var tripList = new List<TripHeader>();
            foreach (var trip in tripEntities) {
                var location = GetLocation(trip.Id);
                var progress = GetProgress(trip);
                tripList.Add(
                    new TripHeader
                    {
                        Id = trip.Id,
                        TripName = trip.TripName,
                        Location = location,
                        Progress = progress
                    });
            }
            return tripList;
        }

        public string GetLocation(int tripId)
        {
            var accommodationIds = DbContext.TripAccommodations.Where(t => t.TripId == tripId).ToList();
            var accommodation = DbContext.Accommodations.Where(a => a.Id == accommodationIds[accommodationIds.Count - 1].AccommodationId).FirstOrDefault();

            return accommodation.Location;
        }

        public string GetProgress(Trip trip)
        {
            var now = System.DateTime.Now;
            var progress = trip.Ends - now;
            return progress.Days.ToString();
        }

        // Update

        public void UpdateExistingTrip(CreateTripHeader trip)
        {
            var existingTrip = DbContext.Trips.Where(t => t.Id == trip.Id).FirstOrDefault();
            existingTrip.TripName = trip.TripName;
            existingTrip.Starts = trip.Starts;
            existingTrip.Ends = trip.Ends;

            var documentOfTrip = DbContext.Documents.Where(d => d.Id == existingTrip.DocumentId).FirstOrDefault();
            documentOfTrip.IdCard = trip.TripDetails.IDCard;
            documentOfTrip.InternationalPassport = trip.TripDetails.InternationalPassport;
            documentOfTrip.DrivingLicense = trip.TripDetails.DrivingLicense;
            documentOfTrip.HealthCard = trip.TripDetails.HealthCard;
            
            DbContext.Documents.Update(documentOfTrip);
            DbContext.Trips.Update(existingTrip);
            DbContext.SaveChanges();
        }

        // Delete
        public void DeleteTripById(int tripId)
        {
            // TODO cascade deleting
        }

    }
}
