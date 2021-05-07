using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class TripAccommodation
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int AccommodationId { get; set; }
    }
}
