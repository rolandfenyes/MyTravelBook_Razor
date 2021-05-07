using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class TravelParticipant
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public int UserId { get; set; }
    }
}
