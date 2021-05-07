using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Accommodation
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public float PricePerNight { get; set; }
    }
}
