using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Trip
    {
        public int Id { get; set; }
        public string TripName { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string Description { get; set; }
        public int TripOwnerId { get; set; }
    }
}
