using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class AccommodationParticipant
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
    }
}
