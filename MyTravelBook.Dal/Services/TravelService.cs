using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTravelBook.Dal.Services
{
    public class TravelService
    {
        public MyDbContext DbContext { get; set; }
        public TravelService(MyDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // Create

        public void CreateNewTravel(TravelHeader travelHeader)
        {
            var travel = new Travel
            {
                Departure = travelHeader.Departure,
                Destination = travelHeader.Destination,
                TransportType = (TravellingType)int.Parse(travelHeader.TransportType),
                TicketPrice = travelHeader.TicketPrice,
                SeatPrice = travelHeader.SeatPrice,
                LuggagePrice = travelHeader.LuggagePrice,
                Distance = travelHeader.Distance,
                Consumption = travelHeader.Consumption,
                FuelPrice = travelHeader.FuelPrice
            };
            DbContext.Travels.Add(travel);
            DbContext.SaveChanges();

            DbContext.TripTravels.Add(
                new TripTravel
                {
                    TravelId = travel.Id,
                    TripId = (int)travelHeader.TripId
                });

            foreach (var id in travelHeader.ParticipantIds)
            {
                DbContext.TravelParticipants.Add(
                    new TravelParticipant
                    {
                        UserId = id,
                        TravelId = travel.Id
                    });
            }

            DbContext.SaveChanges();

        }

        // Read

        public TravelHeader GetTravel(int travelId)
        {
            var travel = DbContext.Travels.Where(t => t.Id == travelId).FirstOrDefault();
            var participants = DbContext.TravelParticipants.Where(t => t.TravelId == travelId).ToList();
            var participantIds = new List<int>();
            foreach (var participant in participants)
            {
                participantIds.Add(participant.UserId);
            }
            return new TravelHeader
                {
                    Id = travel.Id,
                    TripId = null,
                    Departure = travel.Departure,
                    Destination = travel.Destination,
                    TransportType = travel.TransportType.ToString(),
                    TicketPrice = travel.TicketPrice,
                    SeatPrice = travel.SeatPrice,
                    LuggagePrice = travel.LuggagePrice,
                    Distance = travel.Distance,
                    Consumption = travel.Consumption,
                    FuelPrice = travel.FuelPrice,
                    ParticipantIds = participantIds
                };
        }

        // Update

        public void UpdateExistingTravel(TravelHeader travelHeader)
        {
            var travel = DbContext.Travels.Where(t => t.Id == travelHeader.Id).FirstOrDefault();
            travel.Departure = travelHeader.Departure;
            travel.Destination = travelHeader.Destination;
            travel.TransportType = (TravellingType)int.Parse(travelHeader.TransportType);
            travel.TicketPrice = travelHeader.TicketPrice;
            travel.SeatPrice = travelHeader.SeatPrice;
            travel.LuggagePrice = travelHeader.LuggagePrice;
            travel.Distance = travelHeader.Distance;
            travel.Consumption = travelHeader.Consumption;
            travel.FuelPrice = travelHeader.FuelPrice;

            DbContext.Travels.Update(travel);

            var travelParticipants = DbContext.TravelParticipants.Where(t => t.TravelId == travelHeader.Id).ToList();
            foreach (var id in travelHeader.ParticipantIds)
            {
                var isIdInTravel = travelParticipants.Find(t => t.UserId == id);
                if (isIdInTravel == null)
                {
                    DbContext.TravelParticipants.Add(
                        new TravelParticipant
                        {
                            TravelId = travelHeader.Id,
                            UserId = id
                        });
                }
            }
            DbContext.SaveChanges();
        }

        // Delete

        public void DeleteExistingTravel(int travelId)
        {
            // TODO
        }

    }
}
