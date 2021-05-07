using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class AccommodationHeader
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public string Location { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string AccommodationType { get; set; }
        public float PricePerNight { get; set; }
        public List<int>? ParticipantIds { get; set; }
    }
}
