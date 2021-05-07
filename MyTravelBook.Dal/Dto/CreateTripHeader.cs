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
        public TripDetailsHeader TripDetails { get; set; }
        public List<int> ParticipantIds { get; set; }
        public int TripOwnerId { get; set; }
    }
}
