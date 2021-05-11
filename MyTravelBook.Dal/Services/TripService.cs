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

        public int CreateNewTrip(CreateTripHeader trip)
        {
            var tripEntity = new Trip
            {
                TripName = trip.TripName,
                Starts = new DateTime(int.Parse(trip.StartYear), int.Parse(trip.StartMonth), int.Parse(trip.StartDay)),
                Ends = new DateTime(int.Parse(trip.EndYear), int.Parse(trip.EndMonth), int.Parse(trip.EndDay)),
                Description = trip.Description,
                TripOwnerId = trip.TripOwnerId
            };
            DbContext.Trips.Add(tripEntity);
            DbContext.SaveChanges();

            DbContext.TripParticipants.Add(
                    new TripParticipants
                    {
                        TripId = tripEntity.Id,
                        UserId = tripEntity.TripOwnerId
                    });

            foreach (var p in trip.ParticipantIds)
            {
                DbContext.TripParticipants.Add(
                    new TripParticipants
                    {
                        TripId = tripEntity.Id,
                        UserId = p
                    });
            }

            DbContext.SaveChanges();

            return tripEntity.Id;
        }

        public void CreateNewTravel(TravelHeader travel)
        {
            TravelService.CreateNewTravel(travel);
        }

        public void CreateNewAccommodation(AccommodationHeader accommodation)
        {
            AccommodationService.CreateNewAccommodation(accommodation);
        }

        public void CreateNewExpense(ExpenseHeader expense)
        {
            ExpenseService.CreateNewExpense(expense);
        }

        // Read

        public List<TripOverallHeader> GetOverallOfTrip(int tripId, int userId)
        {
            var tripOverallHeaders = new List<TripOverallHeader>();

            var travels = GetTravelsOfTrip(tripId).Travels.Select(t => t.Destination);
            var accommodations = GetAccommodationsOfTrip(tripId).Accommodations.Select(a => a.Location);
            var expenses = GetExpensesOfTrip(tripId).Expenses.Select(e => e.Location);

            var allLocations = travels.Concat(accommodations).Concat(expenses).ToList();
            var locations = new HashSet<string>(allLocations);
            
            foreach (var l in locations)
            {
                var accommodationsOfLocation = GetAccommodationAtLocation(l, tripId, userId);
                var travelsOfLocation = GetTravelsByDestination(l, tripId, userId);
                var expensesOfLocation = GetExpensesAtLocation(l, tripId, userId);

                var nights = accommodationsOfLocation.Select(a => a.Nights).Sum();
                var accommodationPrice = accommodationsOfLocation.Select(a => a.CostPerCapita).Sum();
                var travelCosts = travelsOfLocation.Select(t => t.CostPerCapita).Sum();
                var expenseCosts = expensesOfLocation.Select(e => e.PricePerCapita).Sum();
                var total = accommodationPrice + travelCosts + expenseCosts;

                tripOverallHeaders.Add(
                    new TripOverallHeader
                    {
                        Location = l,
                        Nights = nights,
                        AccommodationPrice = accommodationPrice,
                        TravelCosts = travelCosts == null ? new decimal(0) : (decimal)travelCosts,
                        Expenses = expenseCosts,
                        Total = total == null ? new decimal(0) : (decimal)total
                    });
            }

            return tripOverallHeaders;

        }

        private List<ExpenseHeader> GetExpensesAtLocation(string l, int tripId, int userId)
        {
            var expenses = GetExpensesOfTrip(tripId).Expenses.Where(e => e.Location == l && e.TripId == tripId).ToList();

            return expenses.Where(e => e.ParticipantIds.Contains(userId)).ToList();
        }

        private List<AccommodationHeader> GetAccommodationAtLocation(string location, int tripId, int userId)
        {
            var accommodations = GetAccommodationsOfTrip(tripId).Accommodations.Where(a => a.Location == location && a.TripId == tripId).ToList();

            return accommodations.Where(a => a.ParticipantIds.Contains(userId)).ToList();
        }

        private List<TravelHeader> GetTravelsByDestination(string destination, int tripId, int userId)
        {
            var travels = GetTravelsOfTrip(tripId).Travels.Where(t => t.Destination == destination && t.TripId == tripId).ToList();

            return travels.Where(t => t.ParticipantIds.Contains(userId)).ToList();
        }

        public FriendsHeader GetFriends(int userId)
        {
            var friends = DbContext.Friends.Where(f => f.UserId1 == userId || f.UserId2 == userId).ToList();
            var friendsHeader = new FriendsHeader();
            friendsHeader.FriendsList = new List<FriendHeader>();
            foreach (var friend in friends)
            {
                var id = friend.UserId1 == userId ? friend.UserId2 : friend.UserId1;
                friendsHeader.FriendsList.Add(
                    new FriendHeader
                    {
                        FriendId = id,
                        Nickname = DbContext.Users.Where(u => u.Id == id).FirstOrDefault().Name
                    });
            }
            return friendsHeader;
        }

        public ExpensesHeader GetExpensesOfTrip(int tripId)
        {
            var expenseIds = DbContext.TripExpenses.Where(t => t.TripId == tripId).ToList();
            var expensesOfTrip = new ExpensesHeader();
            expensesOfTrip.Expenses = new List<ExpenseHeader>();
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
            accommodationsOfTrip.Accommodations = new List<AccommodationHeader>();
            foreach (var accommodationId in accommodationIds)
            {
                accommodationsOfTrip.Accommodations.Add(AccommodationService.GetAccommodation(accommodationId.AccommodationId));
            }
            return accommodationsOfTrip;
        }

        public TravelsHeader GetTravelsOfTrip(int tripId)
        {
            var travelIds= DbContext.TripTravels.Where(t => t.TripId == tripId).ToList();
            var travelsOfTrip = new TravelsHeader();
            travelsOfTrip.Travels = new List<TravelHeader>();
            foreach (var travelId in travelIds)
            {
                travelsOfTrip.Travels.Add(TravelService.GetTravel(travelId.TravelId));
            }
            return travelsOfTrip;
        }

        public FriendsHeader GetParticipantsOfTrip(int tripId)
        {
            var participants = DbContext.TripParticipants.Where(t => t.TripId == tripId).ToList();
            var participantHeaders = new FriendsHeader();
            participantHeaders.FriendsList = new List<FriendHeader>();
            foreach (var user in participants)
            {
                participantHeaders.FriendsList.Add(
                    new FriendHeader
                    {
                        FriendId = user.UserId,
                        Nickname = DbContext.Users.Where(u => u.Id == user.UserId).FirstOrDefault().Name
                    });
            }
            return participantHeaders;
        }

        public List<int> GetParticipantIdsOfTrip(int tripId)
        {
            var participants = DbContext.TripParticipants.Where(t => t.TripId == tripId).ToList();
            var participantIds = new List<int>();
            foreach (var p in participants)
            {
                participantIds.Add(p.UserId);
            }
            return participantIds;
        }

        public CreateTripHeader GetFullTrip(int id)
        {
            var trip = DbContext.Trips.Where(t => t.Id == id).FirstOrDefault();
            return new CreateTripHeader
            {
                Id = trip.Id,
                TripName = trip.TripName,
                Starts = trip.Starts,
                Ends = trip.Ends,
                Description = trip.Description,
                ParticipantIds = GetParticipantIdsOfTrip(id),
                TripOwnerId = trip.TripOwnerId
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
            if (accommodationIds.Count != 0)
            {
                var accommodation = DbContext.Accommodations.Where(a => a.Id == accommodationIds[accommodationIds.Count - 1].AccommodationId).FirstOrDefault();
                return accommodation.Location;
            }
            else
            {
                return "Not available";
            }
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
            existingTrip.Description = trip.Description;
            
            DbContext.Trips.Update(existingTrip);
            DbContext.SaveChanges();
        }

        public bool AddFriend(int userId, string nickname)
        {
            var newFriend = DbContext.Users.Where(u => u.Name == nickname).FirstOrDefault();
            // check if not exists
            if (newFriend == null)
            {
                return false;
            }

            var friendList = DbContext.Friends.Where(u => u.UserId1 == userId || u.UserId2 == userId).ToList();
            // check if already a friend
            if (friendList.Select(u => u.UserId1).Contains(newFriend.Id))
            {
                return false;
            }

            DbContext.Friends.Add(
                new Friend
                {
                    UserId1 = userId,
                    UserId2 = newFriend.Id
                });
            DbContext.SaveChanges();
            return true;
        }

        // Delete
        public void DeleteTripById(int tripId)
        {
            // TODO cascade deleting
        }

    }
}
