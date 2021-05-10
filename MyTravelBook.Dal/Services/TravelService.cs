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
                TransportType = (TravellingType)(travelHeader.TransportType),
                TicketPrice = travelHeader.TicketPrice != null ? (float)travelHeader.TicketPrice : 0F,
                SeatPrice = travelHeader.SeatPrice != null ? (float)travelHeader.SeatPrice : 0F,
                LuggagePrice = travelHeader.LuggagePrice != null ? (float)travelHeader.LuggagePrice : 0F,
                Distance = travelHeader.Distance != null ? (float)travelHeader.Distance : 0F,
                Consumption = travelHeader.Consumption != null ? (float)travelHeader.Consumption : 0F,
                FuelPrice = travelHeader.FuelPrice != null ? (float)travelHeader.FuelPrice : 0F
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
                    TripId = DbContext.TripTravels.Where(t => t.TravelId == travelId).FirstOrDefault().TripId,
                    Departure = travel.Departure,
                    Destination = travel.Destination,
                    TransportType = (int)travel.TransportType,
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
            travel.TransportType = (TravellingType)(travelHeader.TransportType);
            travel.TicketPrice = travelHeader.TicketPrice != null ? (float)travelHeader.TicketPrice : 0F;
            travel.SeatPrice = travelHeader.SeatPrice != null ? (float)travelHeader.SeatPrice : 0F;
            travel.LuggagePrice = travelHeader.LuggagePrice != null ? (float)travelHeader.LuggagePrice : 0F;
            travel.Distance = travelHeader.Distance != null ? (float)travelHeader.Distance : 0F;
            travel.Consumption = travelHeader.Consumption != null ? (float)travelHeader.Consumption : 0F;
            travel.FuelPrice = travelHeader.FuelPrice != null ? (float)travelHeader.FuelPrice : 0F;

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
