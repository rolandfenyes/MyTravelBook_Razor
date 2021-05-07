using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class TripParticipants
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int UserId { get; set; }
    }
}
