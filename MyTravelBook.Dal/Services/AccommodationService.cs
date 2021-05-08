using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTravelBook.Dal.Services
{
    public class AccommodationService
    {
        public MyDbContext DbContext { get; set; }

        public AccommodationService(MyDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // Create

        public void CreateNewAccommodation(AccommodationHeader accommodationHeader)
        {
            var accommodation = new Accommodation
            {
                Location = accommodationHeader.Location,
                Starts = accommodationHeader.Starts,
                Ends = accommodationHeader.Ends,
                AccommodationType = (AccommodationType)int.Parse(accommodationHeader.AccommodationType),
                PricePerNight = accommodationHeader.PricePerNight
            };
            DbContext.Accommodations.Add(accommodation);
            DbContext.SaveChanges();

            DbContext.TripAccommodations.Add(
                new TripAccommodation
                {
                    AccommodationId = accommodation.Id,
                    TripId = accommodationHeader.TripId
                });
            foreach (var id in accommodationHeader.ParticipantIds)
            {
                DbContext.AccommodationParticipants.Add(
                    new AccommodationParticipant
                    {
                        AccommodationId = accommodation.Id,
                        UserId = id
                    });
            }
            DbContext.SaveChanges();
        }

        // Read

        public AccommodationHeader GetAccommodation(int accommodationId)
        {
            var accommodation = DbContext.Accommodations.Where(t => t.Id == accommodationId).FirstOrDefault();
            var participants = DbContext.AccommodationParticipants.Where(t => t.AccommodationId == accommodationId).ToList();
            var participantIds = new List<int>();
            foreach (var participant in participants)
            {
                participantIds.Add(participant.UserId);
            }
            return new AccommodationHeader
                {
                    Id = accommodation.Id,
                    TripId = DbContext.TripAccommodations.Where(a => a.AccommodationId == accommodationId).FirstOrDefault().TripId,
                    Location = accommodation.Location,
                    Starts = accommodation.Starts,
                    Ends = accommodation.Ends,
                    AccommodationType = accommodation.AccommodationType.ToString(),
                    PricePerNight = accommodation.PricePerNight,
                    ParticipantIds = participantIds
                };
        }

        // Update

        public void UpdateExistingAccommodation(AccommodationHeader accommodationHeader)
        {
            var accommodation = DbContext.Accommodations.Where(a => a.Id == accommodationHeader.Id).FirstOrDefault();
            accommodation.Location = accommodationHeader.Location;
            accommodation.Starts = accommodationHeader.Starts;
            accommodation.Ends = accommodationHeader.Ends;
            accommodation.AccommodationType = (AccommodationType)int.Parse(accommodationHeader.AccommodationType);
            accommodation.PricePerNight = accommodationHeader.PricePerNight;

            DbContext.Accommodations.Update(accommodation);

            var accommodationParticipants = DbContext.AccommodationParticipants.Where(a => a.AccommodationId == accommodationHeader.Id).ToList();
            foreach (var id in accommodationHeader.ParticipantIds)
            {
                var isIdInTravel = accommodationParticipants.Find(t => t.UserId == id);
                if (isIdInTravel == null)
                {
                    DbContext.AccommodationParticipants.Add(
                        new AccommodationParticipant
                        {
                            AccommodationId = accommodationHeader.Id,
                            UserId = id
                        });
                }
            }
            DbContext.SaveChanges();
        }

        // Delete

        public void DeleteExistingAccommodation(int accommodationId)
        {
            // TODO
        }
    }
}
