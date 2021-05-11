using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class CreateTripHeader
    {
        public int? Id { get; set; }
        public string TripName { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string EndDay { get; set; }
        public string EndMonth { get; set; }
        public string EndYear { get; set; }
        public string Description { get; set; }
        public List<int> ParticipantIds { get; set; }
        public int TripOwnerId { get; set; }
    }
}
