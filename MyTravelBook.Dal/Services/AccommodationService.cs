﻿using MyTravelBook.Dal.Dto;
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
                Starts = new DateTime(int.Parse(accommodationHeader.StartYear), int.Parse(accommodationHeader.StartMonth), int.Parse(accommodationHeader.StartDay)),
                Ends = new DateTime(int.Parse(accommodationHeader.EndYear), int.Parse(accommodationHeader.EndMonth), int.Parse(accommodationHeader.EndDay)),
                AccommodationType = (AccommodationType)(accommodationHeader.AccommodationType),
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
            var nights = CalculateNights(accommodation);
            var totalCost = CalculateCost(accommodation, nights, participantIds.Count, true);
            var costPerCapita = CalculateCost(accommodation, nights, participantIds.Count, false);
            return new AccommodationHeader
                {
                    Id = accommodation.Id,
                    TripId = DbContext.TripAccommodations.Where(a => a.AccommodationId == accommodationId).FirstOrDefault().TripId,
                    Location = accommodation.Location,
                    Starts = accommodation.Starts,
                    Ends = accommodation.Ends,
                    AccommodationType = (int)accommodation.AccommodationType,
                    PricePerNight = accommodation.PricePerNight,
                    ParticipantIds = participantIds,
                    Nights = nights,
                    TotalCost = totalCost,
                    CostPerCapita = costPerCapita
                };
        }

        public decimal CalculateCost(Accommodation accommodation, int nights, int numOfParticipants, bool isTotal)
        {
            var totalCost = new decimal(accommodation.PricePerNight * nights);
            if (isTotal)
            {
                return totalCost;
            }
            else
            {
                return totalCost / numOfParticipants;
            }
        }

        public int CalculateNights(Accommodation accommodation)
        {
            return accommodation.Ends.DayOfYear - accommodation.Starts.DayOfYear;
        }

        public FriendsHeader GetParticipantsOfAccommodation(int accommodationId)
        {
            var participants = DbContext.AccommodationParticipants.Where(t => t.AccommodationId == accommodationId).ToList();
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

        // Update

        public void UpdateExistingAccommodation(AccommodationHeader accommodationHeader)
        {
            var accommodation = DbContext.Accommodations.Where(a => a.Id == accommodationHeader.Id).FirstOrDefault();
            accommodation.Location = accommodationHeader.Location;
            accommodation.Starts = accommodationHeader.Starts;
            accommodation.Ends = accommodationHeader.Ends;
            accommodation.AccommodationType = (AccommodationType)(accommodationHeader.AccommodationType);
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
