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

        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string EndDay { get; set; }
        public string EndMonth { get; set; }
        public string EndYear { get; set; }
    }
}
